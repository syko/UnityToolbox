using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace Syko.UnityToolbox.UI
{
  public class OptionPicker : MonoBehaviour
  {
    public TMP_Text label;
    public Button prevButton;
    public Button nextButton;
    public string[] options;
    public int value { get; private set; }
    public UnityEvent onOptionPicked;

    // Start is called before the first frame update
    void Start()
    {
      updateUI();
    }

    private void updateUI()
    {
      label.text = options.Length > 0 ? options[value] : "";
    }

    public void SetOptions(string[] options)
    {
      this.options = options;
      updateUI();
    }

    public void SetOptions(string[] options, int value)
    {
      this.value = value == -1 ? 0 : value;
      SetOptions(options);
    }

    public void SetValue(int value)
    {
      this.value = value;
      updateUI();
    }

    public void OnClickPrev()
    {
      SetValue((value - 1 + options.Length) % options.Length);
      onOptionPicked?.Invoke();
    }

    public void OnClickNext()
    {
      SetValue((value + 1 + options.Length) % options.Length);
      onOptionPicked?.Invoke();
    }
  }
}
