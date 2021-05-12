using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MiniGameType2 : MiniGame
{
    public MiniGameImage[][] images;
    GameObject imageCellGameObject;
    public string[] oppositeEmotionNames;
    private Transform progress;

    public MiniGameType2(string name, string explanation, Transform sceneElement, string shortExplanation, Sprite faceInformation, MiniGameImage[][] images, string[] oppositeEmotionNames) : base(name, explanation, sceneElement, shortExplanation, faceInformation)
    {
        this.images = images;
        imageCellGameObject = sceneElement.Find("MiniGame").Find("imageCell").gameObject;
        this.oppositeEmotionNames = oppositeEmotionNames;
        this.progress = GameObject.Find("MinigameCanvas").transform.Find("Image/Progress");
    }

    private void cleanCells()
    {
        for (int i = 0; i < 12; i++)
        {
            Transform cellContainer = sceneElement.Find("MiniGame/Images/image" + i);
            if (cellContainer.childCount == 0)
            {
                GameObject newImageCell = GameObject.Instantiate(imageCellGameObject, cellContainer);
                newImageCell.name = "image" + i;
                newImageCell.transform.localPosition = new Vector3(0, 0, 0);
                newImageCell.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
                newImageCell.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            }

            Transform cellEmotionsContainer = sceneElement.Find("MiniGame/Emotions/EmotionsContainer/Images/image" + i);
            if (cellEmotionsContainer.childCount > 0)
            {
                foreach (Transform emotionCell in cellEmotionsContainer)
                {
                    GameObject.Destroy(emotionCell.gameObject);
                }
            }

            Transform cellNeutralContainer = sceneElement.Find("MiniGame/Emotions/NeutralContainer/Images/image" + i);
            if (cellNeutralContainer.childCount > 0)
            {
                foreach (Transform emotionCell in cellNeutralContainer)
                {
                    GameObject.Destroy(emotionCell.gameObject);
                }
            }
        }
    }

    public override void SetupMiniGame()
    {
        progress.gameObject.SetActive(true);
        sceneElement.Find("MiniGame/Emotions/EmotionsContainer/Text").GetComponent<Text>().text = PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].name;
        sceneElement.Find("MiniGame/Emotions/NeutralContainer/Text").GetComponent<Text>().text = oppositeEmotionNames[this.currentChallenge];
        ImageSelection.selectedImage0 = PlayerInfo.NOT_SELECTED_ANSWEAR;
        cleanCells();
        int i = 0;
        while (i < images[this.currentChallenge].Length)
        {
            Transform cellContainer = sceneElement.Find("MiniGame/Images/image" + i);
            cellContainer.gameObject.SetActive(true);
            Transform cellItem = cellContainer.Find("image" + i);
            cellItem.gameObject.SetActive(true);
            cellItem.GetComponent<Image>().sprite = images[this.currentChallenge][i].image;
            cellItem.GetComponent<DraggableImageCellInfo>().isMainEmotion = images[this.currentChallenge][i].isCorrect;
            i++;
        }
        while (i < 12)
        {
            Transform cellContainer = sceneElement.Find("MiniGame/Images/image" + i);
            cellContainer.gameObject.SetActive(false);
            Transform cellItem = cellContainer.Find("image" + i);
            cellItem.gameObject.SetActive(false);
            // sceneElement.Find("MiniGame/Images/image" + i + "/image" + i).gameObject.SetActive(false);
            i++;
        }


        int k = 0;
        while (k <= this.currentChallenge)
        {
            progress.Find("p" + k).GetComponent<Image>().sprite = PlayerInfo.miniGameDoneImg;
            k++;
        }
        while (k < images.Length)
        {
            progress.Find("p" + k).GetComponent<Image>().sprite = PlayerInfo.miniGameToDoImg;
            k++;
        }
    }

    public override bool HasNextChallenge()
    {
        return this.currentChallenge < (images.Length - 1);
    }

    public override void NextChallenge()
    {
        this.currentChallenge++;
        SetupMiniGame();
    }

    public override void FinishGame()
    {
        progress.gameObject.SetActive(false);
        cleanCells();
        sceneElement.Find("MiniGame").gameObject.SetActive(false);
        GameObject.Find("MinigameCanvas/Image/shortExplanation").gameObject.SetActive(false);
        this.currentChallenge = 0;
    }

    private bool isCellBeingUsed(Transform cell)
    {
        if (cell.childCount == 0)
        {
            return false;
        }

        Transform[] cellChildren = new Transform[cell.childCount];
        for (int i = 0; i < cell.childCount; i++)
        {
            cellChildren[i] = cell.GetChild(i);
        }

        return Array.FindIndex(cellChildren, child => child.gameObject.activeSelf) != -1;
    }

    private static Transform[] getChildren(Transform originalObject)
    {
        Transform[] arrayToReturn = new Transform[originalObject.childCount];
        for (int i = 0; i < originalObject.childCount; i++)
        {
            arrayToReturn[i] = originalObject.GetChild(i);
        }

        return arrayToReturn;
    }

    private static bool isCellWrong(Transform cell)
    {
        return cell.childCount > 0 && !cell.GetChild(0).GetComponent<DraggableImageCellInfo>().isMainEmotion;
    }

    public override MiniGameResponse ValidateAnswear()
    {
        Transform[] imagesCellContainers = getChildren(sceneElement.Find("MiniGame/Images"));
        if (Array.FindIndex(imagesCellContainers, cell => isCellBeingUsed(cell)) != -1)
        {
            return new MiniGameResponse(PlayerInfo.NOT_SELECTED_ANSWEAR, "Separe todas as imagens arrastando!");
        }

        Transform[] answearImagesCellContainers = getChildren(sceneElement.Find("MiniGame/Emotions/EmotionsContainer/Images"));
        if (Array.FindIndex(answearImagesCellContainers, cell => isCellWrong(cell)) != -1)
        {
            return new MiniGameResponse(PlayerInfo.WRONG_ANSWEAR, "Pelo menos uma imagem foi colocada no grupo errado. Tente novamente!");
        }
        return new MiniGameResponse(PlayerInfo.CORRECT_ANSWEAR, "Parabéns! Você acertou!");
    }

    public override void ClearImagesColors() { }
}