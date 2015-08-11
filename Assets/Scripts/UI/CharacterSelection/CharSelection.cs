using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharSelection : MonoBehaviour
{
    [Header("Boys")]
    [SerializeField]
    private List<GameObject> MaleMeshs;
    [SerializeField]
    private List<Material> MaleHairColors;   
    [SerializeField]
    private GameObject BoysUI;

    private int m_maleCurBody = 0;
    private int m_maleCurHairColor = 0;

    [Header("Girl")]
    [SerializeField]
    private List<Material> FemaleBootsColors;
    [SerializeField]
    private GameObject GirlsUI;
    [SerializeField]
    private SkinnedMeshRenderer GirlBoots;  

    private int m_femaleCurBootsColor = 0;

    [Header("Camera Positions")]
   // [SerializeField]
    //private Transform StartPos;
    [SerializeField]
    private Transform BoyPos;
    [SerializeField]
    private Transform GirlPos;

    /// <summary>
    /// 0 = none
    /// 1 = boy
    /// 2 = girl
    /// </summary>
    private int curGender = 0;

	void Start () 
    {      	
	}

	void Update () 
    {     
    }

    //Camera
    public void MoveToBoy()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,BoyPos.position,10);
        curGender = 1;
        ShowBoysUI();
    }
    public void MoveToGirl()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, GirlPos.position, 10);
        curGender = 2;
        ShowGirlsUI();
    }

    #region Boys
    //Boys
    public void NextMaleBody()
    {
        int i = MaleMeshs.Count -1;
        if (m_maleCurBody == i)
            m_maleCurBody = 0;
        else
            m_maleCurBody++;

        ActivateCurBody();
    }
    public void PreviousMaleBody()
    {
        int i = MaleMeshs.Count - 1;
        if (m_maleCurBody == 0)
            m_maleCurBody = i;
        else
            m_maleCurBody--;

        ActivateCurBody();
        
    }
    private void ActivateCurBody()
    {
        //Disable All meshes
        for (int e = 0; e < MaleMeshs.Count; e++)
        {
            MaleMeshs[e].SetActive(false);
        }
        //Enable the next Mesh
        MaleMeshs[m_maleCurBody].SetActive(true);
    }
    public void NextMaleHairColor()
    {
        int i = MaleHairColors.Count - 1;
        if (m_maleCurHairColor == i)
            m_maleCurHairColor = 0;
        else
            m_maleCurHairColor++;
        ChangeMaleHairColor();
        //Debug.Log("Next Color" + m_maleCurHairColor);
    }
    public void PreviousMaleHairColor()
    {
        int i = MaleHairColors.Count - 1;
        if (m_maleCurHairColor == 0)
            m_maleCurHairColor = i;
        else
            m_maleCurHairColor--;
        ChangeMaleHairColor();
        //Debug.Log("Previous Color" + m_maleCurHairColor);
    }
    private void ChangeMaleHairColor()
    {
        for (int i = 0; i < MaleMeshs.Count;i++)
        {
            MaleMeshs[i].GetComponent<SkinnedMeshRenderer>().material = MaleHairColors[m_maleCurHairColor];
        }
            
        
    }
    #endregion

    #region Girls
    //Girls
    public void NextGirlsBoots()
    {
        int i = FemaleBootsColors.Count -1;
        if (m_femaleCurBootsColor == i)
            m_femaleCurBootsColor = 0;
        else
            m_femaleCurBootsColor++;
        ChangeGirlsBootsColor();
    }
    public void PreviousGirlsBoolts()
    {
        int i = FemaleBootsColors.Count -1;
        if (m_femaleCurBootsColor == 0)
            m_femaleCurBootsColor = i;
        else
            m_femaleCurBootsColor--;
        ChangeGirlsBootsColor();
    }
    private void ChangeGirlsBootsColor()
    {
        GirlBoots.material = FemaleBootsColors[m_femaleCurBootsColor];
    }
    
    #endregion

    #region UI To Show

    private void ShowBoysUI()
    {
        BoysUI.SetActive(true);
        GirlsUI.SetActive(false);
    }

    private void ShowGirlsUI()
    {
        BoysUI.SetActive(false);
        GirlsUI.SetActive(true);
    }

    #endregion

    public void CreateCharacter()
    {
        if (curGender == 0)
            return;
    }
}
