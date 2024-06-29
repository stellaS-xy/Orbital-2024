using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

class AnimalConfig
{
    public List<string> AnimalList;
}


public class CardManager : MonoBehaviour
{
    GameObject _originCard;
    int row = 4;
    int col = 4;
    float _cardH = 1.5f;
    float _spaceX = 0.5f;
    float _spaceY = 0.5f;
    public static CardManager Instance;
    List<Card> _cardList;
    Card _currentTarget;
    List<Card> _compareCardList;
    List<Card> _rotateCardList;


    int step;
    bool _gameover;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        _originCard = Resources.Load<GameObject>("Card");
        _cardList = new List<Card>();
        _compareCardList = new List<Card>();
        _rotateCardList = new List<Card>();

        string config = Resources.Load<TextAsset>("config").text;
        var animals = JsonUtility.FromJson<AnimalConfig>(config);



        List<string> randomList = new List<string>();
        List<string> originList = new List<string>();
        originList.AddRange(animals.AnimalList);
        //int count = originList.Count;
        int count = 8;
        for (int i = 0; i < count; i++)
        {
            int random = Random.Range(0, originList.Count);
            randomList.Add(originList[random]);
            originList.RemoveAt(random);
        }

        originList.AddRange(animals.AnimalList);
        for (int i = 0; i < count; i++)
        {
            int random = Random.Range(0, originList.Count);
            randomList.Add(originList[random]);
            originList.RemoveAt(random);
        }


        Vector3 offset = Vector3.down * (row) / 2 * (_cardH + _spaceY) + Vector3.left * (col) / 2 * (_cardH + _spaceX);
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                GameObject cloneCard = Instantiate(_originCard);
                var card = cloneCard.GetComponent<Card>();
                card.Initial(randomList[i * col + j]);
                _cardList.Add(card);
                cloneCard.transform.position = transform.position + offset + Vector3.up * i * (_cardH + _spaceY) + Vector3.right * j * (_cardH + _spaceX);
                cloneCard.transform.SetParent(transform);

            }
        }

    }





    // Update is called once per frame
    void Update()
    {
        if (_gameover)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }


        MouseDetect();
        MouseInput();
    }

    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentTarget != null && _rotateCardList.Count < 2 && !_rotateCardList.Contains(_currentTarget))
            {
                step++;
                //UIManager.Instance.ShowStep(step);
                _currentTarget.Rotate();

            }
        }
    }

    public void AddCard(Card card)
    {
        _rotateCardList.Add(card);
    }


    public void CompareCards(Card card)
    {
        _compareCardList.Add(card);
        if (_compareCardList.Count == 2)
        {
            if (_compareCardList[0].AnimalName == _compareCardList[1].AnimalName)
            {
                _compareCardList[0].SwitchState(StateEnum.matched);
                _compareCardList[1].SwitchState(StateEnum.matched);
                Clear();
                if (IsVictory())
                {
                    Victory();
                }
            
            
            
            
            }
            else
            {
                _compareCardList[0].Rotate(false);
                _compareCardList[1].Rotate(false);
            }
        }

    }

    private void Victory()
    {
        _gameover = true;
    }

    public void Clear()
    {
        _compareCardList.Clear();
        _rotateCardList.Clear();
    }

    private void MouseDetect()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(mouseRay,out hitInfo))
        {
            Card card = hitInfo.transform.GetComponent<Card>();
            if (card!=null)
            {
                if (_currentTarget!=null&& _currentTarget!=card)
                {
                    _currentTarget.Normal();
                }
                _currentTarget = card;
                _currentTarget.Highlight();
            }


        }
        else if (_currentTarget!=null)
        {
            _currentTarget.Normal();
            _currentTarget = null;
        }
    }

    bool IsVictory()
    {

        bool isVictory = true;
        for (int i = 0; i < _cardList.Count; i++)
        {
            if (_cardList[i].CurrentState== StateEnum.unmatched)
            {
                isVictory = false;
            }
        }
        return isVictory;
    }
}

