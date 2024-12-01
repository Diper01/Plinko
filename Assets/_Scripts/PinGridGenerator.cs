using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PinGridGenerator : MonoBehaviour
{
    public GameObject pinPrefab;
    [HideInInspector] public int rows = 10;

    [SerializeField] private float _spacing = 1.6f;

    [SerializeField] private Transform _targetsParent;
    [SerializeField] private GameObject _targetPrefab;

    private List<GameObject> _pins = new();

    private void Awake()
    {
        GeneratePins();
    }

    public void GeneratePins()
    {
        ClearPins();
        CreatePins();
        ClearTargets();
        GenerateAllTargets();
    }

    private void ClearPins()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
            Destroy(transform.GetChild(i).gameObject);

        _pins.Clear();
    }

    private void CreatePins()
    {
        for (int i = 2; i < rows; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                Vector3 position = new Vector3(j * _spacing - i * _spacing / 2, -i * _spacing, 0);
                var gm = Instantiate(pinPrefab, position, Quaternion.identity, transform);
                _pins.Add(gm);
            }
        }
    }

    private void ClearTargets()
    {
        for (int i = _targetsParent.childCount - 1; i >= 0; i--)
            Destroy(_targetsParent.GetChild(i).gameObject);
    }

    private void GenerateAllTargets()
    {
        GenerateTargets(GetTargetsData(TargetColor.Green));
        GenerateTargets(GetTargetsData(TargetColor.Yellow));
        GenerateTargets(GetTargetsData(TargetColor.Red));
    }

    private void GenerateTargets(TargetsData targetsData)
    {
        float[] numbers = targetsData.numbers;
        float startX = -rows * _spacing / 2;
        float[] values = new float[rows];
        int centerIndex = rows / 2;
        for (int i = 0; i < rows; i++)
        {
            int offset = Mathf.Abs(centerIndex - i);
            values[i] = numbers[Mathf.Min(offset, numbers.Length - 1)];
        }

        float posY = targetsData.positionY;
        for (int i = 1; i < rows; i++)
        {
            var position = new Vector3(startX + i * _spacing, -(posY), 0);
            var gm = Instantiate(_targetPrefab, position, Quaternion.identity, _targetsParent);
            gm.GetComponent<Renderer>().material.color = targetsData.color;
            gm.GetComponent<Target>().value = values[i];
            gm.layer = targetsData.layer;
            gm.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = values[i] + "";
        }
    }

    #region TargetsData

    private TargetsData GetTargetsData(TargetColor targetColor)
    {
        var targetDateGreen = new TargetsData(new Color(0.34f, 0.69f, 0.34f, 1f), new[] { 0.5f, 1.0f, 1.1f, 1.2f, 1.3f, 1.6f, 3.2f, 18f },
            rows * _spacing, 11);
        var targetDateYellow = new TargetsData(new Color(0.94f, 0.77f, 0.18f, 1f), new[] { 0.2f, 0.7f, 1f, 1.6f, 3.2f, 5.6f, 12f, 55f },
            (rows+1)  * _spacing, 12);
        var targetDateRed = new TargetsData(new Color(0.80f, 0.25f, 0.25f, 1f), new[] { 0f, 0.2f, 0.5f, 2.1f, 5.3f, 14f, 49f, 353f },
            (rows+2)  * _spacing, 13);


        return targetColor switch
        {
            TargetColor.Green => targetDateGreen,
            TargetColor.Yellow => targetDateYellow,
            TargetColor.Red => targetDateRed,
            _ => targetDateGreen
        };
    }


    private class TargetsData
    {
        public Color color;
        public float[] numbers;
        public float positionY;
        public int layer;

        public TargetsData(Color color, float[] numbers, float positionY, int layer)
        {
            this.color = color;
            this.numbers = numbers;
            this.positionY = positionY;
            this.layer = layer;
        }
    }

    private enum TargetColor
    {
        Green = 0,
        Yellow = 1,
        Red = 2
    }

    #endregion
}