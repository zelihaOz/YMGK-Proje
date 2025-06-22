using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMeshPro deste�i

public class Manager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;         // Skor yaz�s�
    public TextMeshProUGUI operationText;     // ��lem metni
    public TMP_InputField answerInput;        // Cevap giri�i
    public Button checkButton;                // Kontrol butonu

    private int score = 0;
    private int correctAnswer = 0;
    private bool operationActive = false;     // Yeni i�lem haz�r m�?

    void Start()
    {
        UpdateScoreText();
        operationText.text = "";
        answerInput.text = "";

        // Tekrar eklenmeyi �nlemek i�in �nce ��kar, sonra ekle
        checkButton.onClick.RemoveAllListeners();
        checkButton.onClick.AddListener(CheckAnswer);
        //GenerateOperation(); // OYUN BA�LARKEN �LK ��LEM� �RET
    }

    public void GenerateOperation()
    {
        int num1 = Random.Range(1, 16);
        int num2 = Random.Range(1, 16);
        int op = Random.Range(0, 3); // 0: +, 1: -, 2: *

        string opSymbol = "+";

        switch (op)
        {
            case 0:
                correctAnswer = num1 + num2;
                opSymbol = "+";
                break;
            case 1:
                correctAnswer = num1 - num2;
                opSymbol = "-";
                break;
            case 2:
                correctAnswer = num1 * num2;
                opSymbol = "�";
                break;
        }

        operationText.text = $"{num1} {opSymbol} {num2} = ?";
        answerInput.text = "";
    }

    public void CheckAnswer()
    {
        //answerInput.DeactivateInputField();  //  Focus d��� b�rak (g�ncel veriyi almak i�in)
        answerInput.ForceLabelUpdate();  //  TMP input'u g�ncelle
        string answerText = answerInput.text.Trim();  // bo�luklar� temizle
        Debug.Log("CheckAnswer called, input: " + answerText);

        int playerAnswer;
        if (int.TryParse(answerText, out playerAnswer))
        {
            if (playerAnswer == correctAnswer)
            {
                score += 10;
                operationText.text = "Do�ru! +10 puan";
                operationActive = false;        //  yeni i�lem �retilebilir hale gel
               // GenerateOperation();  // Yeni i�lem sadece do�ru cevapta �retilsin
            }
            else
            {
                score -= 10;
                operationText.text = "Yanl��! -10 puan";
            }

            UpdateScoreText();
            answerInput.text = "";
            answerInput.ActivateInputField();
        }
        else
        {
            operationText.text = "L�tfen ge�erli bir say� gir!";
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Skor: " + score;
    }
    public bool IsReadyForNewOperation()
    {
        return !operationActive;
    }

    public void MarkOperationStarted()
    {
        operationActive = true;
    }
}