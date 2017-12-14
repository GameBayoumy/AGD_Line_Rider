using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedMenu : MonoBehaviour {

    public GameObject advancedMenu;
    public Text menuName;
    public Text menuDescription;
    public Slider sizeSlider;
    public Slider rotationSlider;

    private Vector3 _formerMousePosition;
    private GameObject _ph;
    private PlaceholderScript _phScript;
    private bool _lockScaling;
    private bool _lockRotating;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _formerMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Vector3.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), _formerMousePosition) < 0.5)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null && hit.transform.name == "Placeholder(Clone)")
                {
                    _ph = hit.transform.gameObject;
                    _phScript = hit.transform.gameObject.GetComponent<PlaceholderScript>();
                    UpdateAdvancedMenu();
                    advancedMenu.SetActive(true);
                }
            }
        }

        if (_ph != null)
        {
            if (!_lockScaling)
                _ph.transform.localScale = new Vector3(_ph.transform.localScale.x, sizeSlider.value, _ph.transform.localScale.z);
            if (!_lockRotating)
                _ph.transform.localEulerAngles = new Vector3(_ph.transform.localEulerAngles.x, _ph.transform.localEulerAngles.y, rotationSlider.value);
        }

        else
        {
            advancedMenu.SetActive(false);
        }
    }

    void UpdateAdvancedMenu()
    {
        menuName.text = _phScript.myName;
        menuDescription.text = _phScript.description;

        if (_phScript.canBeScaled)
        {
            sizeSlider.transform.localPosition = new Vector2(-4, -45);
            _lockScaling = false;
        }
        else
        {
            sizeSlider.transform.localPosition = new Vector2(-400, -45);
            _lockScaling = true;
        }

        if (_phScript.canBeRotated)
        {
            rotationSlider.transform.localPosition = new Vector2(-4, -117);
            _lockRotating = false;
        }
        else
        {
            rotationSlider.transform.localPosition = new Vector2(-400, -117);
            _lockRotating = true;
        }

        sizeSlider.value = _ph.transform.localScale.y;

        float theRotation = _ph.transform.localEulerAngles.z;
        if (theRotation > 91)
        {
            theRotation -= 360;
        }
        rotationSlider.value = theRotation;
    }

    public void HideAdvancedMenu()
    {
        advancedMenu.SetActive(false);
    }

}
