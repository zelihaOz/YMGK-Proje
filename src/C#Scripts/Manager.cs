using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMeshPro desteði

public class Manager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;         // Skor yazýsý
    public TextMeshProUGUI operationText;     // Ýþlem metni
    public TMP_InputField answerInput;        // Cevap giriþi
    public Button checkButton;                // Kontrol butonu

    private int score = 0;
    private int correctAnswer = 0;
    private bool operationActive = false;     // Yeni iþlem hazýr mý?

    void Start()
    {
        UpdateScoreText();
        operationText.text = "";
        answerInput.text = "";

        // Tekrar eklenmeyi önlemek için önce çýkar, sonra ekle
        checkButton.onClick.RemoveAllListeners();
        checkButton.onClick.AddListener(CheckAnswer);
        //GenerateOperation(); // OYUN BAÞLARKEN ÝLK ÝÞLEMÝ ÜRET
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
                opSymbol = "×";
                break;
        }

        operationText.text = $"{num1} {opSymbol} {num2} = ?";
        answerInput.text = "";
    }

    public void CheckAnswer()
    {
        //answerInput.DeactivateInputField();  //  Focus dýþý býrak (güncel veriyi almak için)
        answerInput.ForceLabelUpdate();  //  TMP input'u güncelle
        string answerText = answerInput.text.Trim();  // boþluklarý temizle
        Debug.Log("CheckAnswer called, input: " + answerText);

        int playerAnswer;
        if (int.TryParse(answerText, out playerAnswer))
        {
            if (playerAnswer == correctAnswer)
            {
                score += 10;
                operationText.text = "Doðru! +10 puan";
                operationActive = false;        //  yeni iþlem üretilebilir hale gel
               // GenerateOperation();  // Yeni iþlem sadece doðru cevapta üretilsin
            }
            else
            {
                score -= 10;
                operationText.text = "Yanlýþ! -10 puan";
            }

            UpdateScoreText();
            answerInput.text = "";
            answerInput.ActivateInputField();
        }
        else
        {
            operationText.text = "Lütfen geçerli bir sayý gir!";
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