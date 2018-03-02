using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextChat : MonoBehaviour
{

    InputField inputField;
    public Text textbox;
    string textboxx, inputValue;


    /// Startメソッド
    /// InputFieldコンポーネントの取得および初期化メソッドの実行
    void Start()
    {
        inputField = GetComponent<InputField>();
        InitInputField();
    }



    /// <summary>
    /// Log出力用メソッド
    /// 入力値を取得してLogに出力し、初期化
    /// </summary>


    public void InputLogger()
    {
        inputValue = inputField.text;
        textboxx = textboxx + ": " + inputValue+"\n";
        Inputclear();
        textbox.text = textboxx;
        InitInputField();
    }
    void InitInputField()
    {

        // 値をリセット
        inputField.text = "";

        // フォーカス
        inputField.ActivateInputField();
    }

    void Inputclear()
    {
        if (inputValue == "clear" || inputValue == "Clear")
        {
            textboxx = "";
        }
        else if (inputValue == "help")
        {
            textboxx = textboxx + " ･" + "inputValue" + "\n"+
                  " ･" + "inputValue" + "\n"+
                  " ･" + "inputValue" + "\n"+
                  " ･" + "inputValue" + "\n";
        }
    }

}