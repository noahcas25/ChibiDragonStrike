using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject _gameOverCanvas, _optionsCanvas, _startButton, _pauseButton, _tapText;
    [SerializeField] private TextMeshProUGUI _scoreText, _highScoreText, _scoreTextGameOver, _totalCurrencyText;
    [SerializeField] private GameObject _gameOverFlash;

    private void Awake() => Application.targetFrameRate = 60;

    private void OnEnable() {
         GameManager.Instance._scoreChangeEvent.AddListener(ChangeScoreText);
         GameManager.Instance._gameStateEvent.AddListener(GameOver);
    }

    private void OnDisable() {
        GameManager.Instance._scoreChangeEvent.RemoveListener(ChangeScoreText);
        GameManager.Instance._gameStateEvent.RemoveListener(GameOver);
        Time.timeScale = 1f;
    }

    public void StartButton() {
        GameManager.Instance.ChangeGameState(true);
        _startButton.SetActive(false);
        _pauseButton.SetActive(true);
        _tapText.SetActive(false);
    }

    private void GameOver(bool gameState) {
        if(gameState) return;

        StartCoroutine(GameOverCoroutine());
    }

    private IEnumerator GameOverCoroutine() {
        _gameOverFlash.SetActive(true);
        yield return new WaitForSeconds(1f);
        _gameOverCanvas.SetActive(true);
            
        _scoreTextGameOver.text = _scoreText.text;
        _highScoreText.text = GameManager.Instance.GetHighScore() + "";
        _totalCurrencyText.text = GameManager.Instance.GetCurrency() + "";
    }

    public void OpenOptions() { 
        Time.timeScale = 0f;
        _optionsCanvas.SetActive(true);
    }

    public void CloseOptions() {
        Time.timeScale = 1f;
        _optionsCanvas.SetActive(false);
    }

    private void ChangeScoreText(int score) {
        if(_scoreText != null) 
            _scoreText.text = "" + score;
    }

    public void ReplayGame() => GameManager.Instance.ReplayGame();
    
    public void ChangeScene(string sceneName) => GameManager.Instance.ChangeScene(sceneName);
}
