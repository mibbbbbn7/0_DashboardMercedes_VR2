using DashboardMercedes;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingStartBehaviour : BaseMonoBehaviour<ILoadingStartFeatureInternal>
{
    [SerializeField] private CanvasGroup _loadingStartCanvas;

    //=========================================================  EYES
    //========================================================================EYES ON ARE STILL MISSING
    [SerializeField] private RawImage _eyeSX;
    [SerializeField] private RawImage _eyeDX;
    private const float durationEyeStepInitial = 0.75f;
    private const float durationEyeStepDivider = 1.64f;
    private float durationEyeStep = durationEyeStepInitial;
    Texture2D _eyeSXTextureON;
    Texture2D _eyeDXTextureON;

    private int _eyeStep = 0;
    private readonly Vector2[] _eyeSteps = new Vector2[]{
                                    new (-1300f, 0f),
                                    new (-280f, 0f),
                                    new (-500f, 0f),
                                    new (-350f, 0f),
                                    new (-460f, 0f),
                                    new (-380f, 0f),
                                    new (-430f, 0f),
                                    new (-395f, 0f),
                                    new (-410f, 0f),
                                    new (-400f, 0f)
                                };

    [SerializeField] private RawImage _bumper;

    //=========================================================  AMG
    private const float durationAmgStep = 0.3f;
    private int _amgStep = 0;
    private const int amgTotalNumberOfSteps = 4;

    [SerializeField] private RawImage _amgSegment_1;
    [SerializeField] private RawImage _amgSegment_2;
    [SerializeField] private RawImage _amgSegment_3;
    [SerializeField] private RawImage _amgSegment_4;
    [SerializeField] private RawImage _amgSegment_5;
    private RawImage[] _amgSegmentsGroup;

    protected Client _client;
    protected IBroadcaster _broadcaster;

    protected override void ManagedAwake()
    {
        base.ManagedAwake();

        _amgSegmentsGroup = new RawImage[]{
            _amgSegment_1,
            _amgSegment_2,
            _amgSegment_3,
            _amgSegment_4,
            _amgSegment_5
        };
        

        _eyeDXTextureON = Resources.Load<Texture2D>(LoadingStartData.EYE_DX_TEXTURE_ON_PATH);
        _eyeSXTextureON = Resources.Load<Texture2D>(LoadingStartData.EYE_SX_TEXTURE_ON_PATH);

        _bumper.color = new Color(1f, 1f, 1f, 0f);

        _client = Client.Instance;
        _broadcaster = _client.Services.Get<IBroadcaster>();

        StartCoroutine(AnimationStepSxEyeOff(_eyeSX.rectTransform, _eyeSteps[_eyeStep], _eyeSteps[1 + _eyeStep]));
        StartCoroutine(AnimationAlphaAmgSegment(_amgSegmentsGroup, _amgSegment_1.color, _amgSegment_1.color.a));
    }

    protected override void ManagedStart()
    {
        base.ManagedStart();
    }

    private IEnumerator AnimationStepSxEyeOff(RectTransform eyeTransform, Vector2 startPosition, Vector2 targetPosition)
    {
        float time = 0f;

        while (time < durationEyeStep)
        {
            float t = time / durationEyeStep;
            t *= (2 - t);  // Ease-Out
            eyeTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
            time += Time.deltaTime;
            MirrorSxEyePositionOnDx();
            yield return null;
        }

        eyeTransform.anchoredPosition = targetPosition;
        durationEyeStep /= durationEyeStepDivider; //Duration of animation becomes smaller at eachstep
        _eyeStep += 1;
        if (_eyeStep < (_eyeSteps.Length - 1))
        {
            StartCoroutine(AnimationStepSxEyeOff(_eyeSX.rectTransform, _eyeSteps[_eyeStep], _eyeSteps[1 + _eyeStep]));
        }
        else
        {
            yield return new WaitForSeconds(0.3f);

            StartCoroutine(EyesOffBecomeTransparent());
        }
    }

    private IEnumerator EyesOffBecomeTransparent()
    {
        float time = 0f;
        float timeToBecameTransparent = 0.2f;

        while (time < timeToBecameTransparent)
        {
            float t = time / timeToBecameTransparent;
            t *= (2 - t);  // Ease-Out
            float newAlpha = Mathf.Lerp(1f, 0f, t);//start alpha 1, end alpha 0
            time += Time.deltaTime;
            _eyeDX.color = new Color(_eyeDX.color.r, _eyeDX.color.g, _eyeDX.color.b, newAlpha);
            _eyeSX.color = new Color(_eyeSX.color.r, _eyeSX.color.g, _eyeSX.color.b, newAlpha);
            yield return null;
        }

        _eyeDX.color = new Color(_eyeDX.color.r, _eyeDX.color.g, _eyeDX.color.b, 0f);
        _eyeSX.color = new Color(_eyeSX.color.r, _eyeSX.color.g, _eyeSX.color.b, 0f);
        _eyeSX.texture = _eyeSXTextureON;
        _eyeDX.texture = _eyeDXTextureON;
        StartCoroutine(EyesOnBecomeOpaque());
        _broadcaster.Broadcast(new LoadingStartBeginEvent());
    }

    private IEnumerator EyesOnBecomeOpaque()
    {
        float time = 0f;
        float timeToBecameTransparent = 0.3f;

        while (time < timeToBecameTransparent)
        {
            float t = time / timeToBecameTransparent;
            t *= (2 - t);  // Ease-Out
            float newAlpha = Mathf.Lerp(0f, 0.8f, t);//start alpha 1, end alpha 0
            time += Time.deltaTime;
            _eyeDX.color = new Color(_eyeDX.color.r, _eyeDX.color.g, _eyeDX.color.b, newAlpha);
            _eyeSX.color = new Color(_eyeSX.color.r, _eyeSX.color.g, _eyeSX.color.b, newAlpha);
            _bumper.color = new Color(1f, 1f, 1f, newAlpha);
            yield return null;
        }

        _bumper.color = new Color(1f, 1f, 1f, 0.8f);
        _eyeDX.color = new Color(_eyeDX.color.r, _eyeDX.color.g, _eyeDX.color.b, 0.8f);
        _eyeSX.color = new Color(_eyeSX.color.r, _eyeSX.color.g, _eyeSX.color.b, 0.8f);
    }

    private void MirrorSxEyePositionOnDx()
    {
        _eyeDX.rectTransform.anchoredPosition = new Vector2(-_eyeSX.rectTransform.anchoredPosition.x, _eyeSX.rectTransform.anchoredPosition.y);
    }
    
    private IEnumerator AnimationAlphaAmgSegment(RawImage[] segments, Color baseColor, float startAlphaValue)
    {
        
        float time = 0f;
        Color startColorAlpha = baseColor;
        startColorAlpha.a = startAlphaValue;

        Color targetColorAlpha = baseColor;
        targetColorAlpha.a = startAlphaValue > 0f ? 0f : 1f;


        while (time < durationAmgStep)
        {
            float t = time / durationAmgStep; // Lineare
            segments[_amgStep].color = Color.Lerp(startColorAlpha, targetColorAlpha, t);

            time += Time.deltaTime;
            yield return null;
        }

        segments[_amgStep].color = targetColorAlpha;

        _amgStep = _amgStep < amgTotalNumberOfSteps ? (_amgStep + 1) : 0;

        StartCoroutine(AnimationAlphaAmgSegment(segments, baseColor, segments[_amgStep].color.a));
    }

    protected override void ManagedUpdate()
    {
        base.ManagedUpdate();


        //CREDO SIA APPRIOPRIATO METTERLO QUA:
        // aspettare che il menu broadcasti la sua istanziazione cosi da distruggere il
        // il loading screen
    }
}