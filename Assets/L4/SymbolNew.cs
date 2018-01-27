using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SymbolNew : MonoBehaviour
{

	public enum SymbolState
	{
		Close,
		Idel,
		Highlight,
	}

	private SymbolState _thisState;

	private TargetSymbolNew _myManager;

	public ParticleSystem thisEffects;
	
	public int _id;
   
    private int _value;
    public int value
    {
        get { return _value; }
    }


	public void Init()
	{
		_thisState = SymbolState.Close;
		GetValue ();
	}

	//识别Value
	private void GetValue()
    {
        switch(this.gameObject.name)
        {
		case "riddle_1_Standby":
                _value = 0;
                break;
		case "riddle_2_Standby":
                _value = 1;
                break;
		case "riddle_3_Standby":
                _value = 2;
                break;
		case "riddle_4_Standby":
                _value = 3;
                break;
		case "riddle_5_Standby":
                _value = 4;
                break;
		case "riddle_6_Standby":
                _value = 5;
                break;
		case "riddle_7_Standby":
                _value = 6;
                break;
		case "riddle_8_Standby":
                _value = 7;
                break;
		case "riddle_9_Standby":
                _value = 8;
                break;
		case "riddle_10_Standby":
			_value = 9;
			break;
            default:
                _value = -1;
                break;
        }
    }

	public void SetManager(TargetSymbolNew Manager)
	{
		_myManager = Manager;
	}

	public void SetState(SymbolState _state)
	{
		_thisState = _state;
		switch (_thisState) {
		case SymbolState.Close:
			{
				
			}
			break;
		case SymbolState.Highlight:
			{
//				Highlight ();
			}
			break;
		case SymbolState.Idel:
			{

			}
			break;
		default:
			break;
		}
	}

	public void SetIsTarget()
	{
		if(_thisState==SymbolState.Idel)
		{
			SetState (SymbolState.Highlight);
	    	_myManager.InputSymbol (this);
        }
    }


	public void Highlight()
	{
		thisEffects.gameObject.SetActive (true);
		thisEffects.Stop ();
		thisEffects.Play ();
	}

    public void ResetSymbol()
    {
		thisEffects.Stop ();
		thisEffects.gameObject.SetActive (false);
    }
}
