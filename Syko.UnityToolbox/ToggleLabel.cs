using UnityEngine;
using UnityEngine.UI;

namespace Syko.UnityToolbox.UI
{
  public class ToggleLabel : MonoBehaviour
  {
    public Toggle toggle;
    public Color offColor = Color.gray;
    public Color onColor = Color.white;

    void Start()
    {
      toggle.onValueChanged.AddListener(delegate { UpdateUI(); });
      UpdateUI();
    }

    public void UpdateUI()
    {
      GetComponent<CanvasRenderer>().SetColor(toggle.isOn ? onColor : offColor);
    }
  }
}
