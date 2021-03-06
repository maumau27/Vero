﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static int processoId = 0;
    public static int nivelId = 0;
    public static int caseId = 0;
    public static int scoreId = 0;

    [Serializable]
    public class CenaInicial
    {
        public int cenaId;
    }

    public enum Etapa
    {
        HubProcesso,
        HubNivel,
        HubCase,
        Case, 
        Score
    };

	// Use this for initialization
	void Start () {
        DataStorage.LoadProcessoData();
        print(DataStorage.getCaseMaxScore(1));
        LoadCenaInicial();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void ProgredirEtapa(int novaEtapaId)
    {
        if (processoId == 0) processoId = novaEtapaId;
        else if (nivelId == 0) nivelId = novaEtapaId;
        else if (caseId == 0) caseId = novaEtapaId;
        else if (scoreId == 0) scoreId = novaEtapaId;
        DataStorage.LoadCenaData();
        SceneManager.LoadScene(DataStorage.getCenaNome());
    }

    public static void RegredirEtapa()
    {
        if (scoreId != 0) scoreId = 0;
        else if(caseId != 0) caseId = 0;
        else if (nivelId != 0) nivelId = 0;
        else if (processoId != 0) processoId = 0;
        if (processoId != 0)
        {
            DataStorage.LoadCenaData();
            SceneManager.LoadScene(DataStorage.getCenaNome());
        }
    }

    public static void ProgredirEtapa(int novaEtapaId, string cenaNome)
    {
        if (processoId == 0) processoId = novaEtapaId;
        else if (nivelId == 0) nivelId = novaEtapaId;
        else if (caseId == 0) caseId = novaEtapaId;
        else if (scoreId == 0) scoreId = novaEtapaId;
        DataStorage.LoadCenaData();
        SceneManager.LoadScene(cenaNome);
    }

    public static void RegredirEtapa(string cenaNome)
    {
        if (scoreId != 0) scoreId = 0;
        else if (caseId != 0) caseId = 0;
        else if (nivelId != 0) nivelId = 0;
        else if (processoId != 0) processoId = 0;
        if (processoId != 0)
        {
            DataStorage.LoadCenaData();
            SceneManager.LoadScene(cenaNome);
        }
    }

    public static Etapa getEtapa()
    {
        if (scoreId != 0) return Etapa.Score;
        else if (caseId != 0) return Etapa.Case;
        else if (nivelId != 0) return Etapa.HubCase;
        else if (processoId != 0) return Etapa.HubNivel;
        else return Etapa.HubProcesso;
    }

    public static int getEtapaId()
    {
        if (scoreId != 0) return scoreId;
        else if (caseId != 0) return caseId;
        else if (nivelId != 0) return nivelId;
        else return processoId;
    }

    public static void LoadCenaInicial()
    {
        CenaInicial cenaInicial;
        string json = System.IO.File.ReadAllText("Json/CenaInicial.txt");

        cenaInicial = JsonUtility.FromJson<CenaInicial>(json);

        ProgredirEtapa(cenaInicial.cenaId);
    }

    public static string getEtapaFileString()
    {
        return "Json/" + getEtapa() + "_" + getEtapaId() + ".txt";
    }
}
