using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TextUI = TMPro.TextMeshProUGUI;


/// <summary>
/// Faz a ponte entre os dados do Jogador e a Hud.
/// </summary>
// Deixa o script Jogador mais limpo.

public class InterfaceDeJogador : MonoBehaviour {
    [Header( "Exibição de valores" )]
    [SerializeField] TextUI JogadorTitulo;
    [SerializeField] TextUI VidaValor;
    [SerializeField] TextUI ManaValor;
    [SerializeField] TextUI ForcaValor;

    [Header( "Botões" )]
    [SerializeField] GameObject Bloqueadorbotoes;

    public Button BotaoPinturaDeGuerra;
    public Button BotaoEspadaAfiada;
    public Button BotaoDescanso;

    [Header( "Efeitos Ativos" )]
    [SerializeField] Transform EfeitosParent;
    [SerializeField] GameObject PrefabEfeito;

    /// <summary>
    /// Quando true, um painel sobrepõe os botões do jogador bloqueando-os de serem usados.
    /// </summary>
    public bool BotoesBloqueados { set { Bloqueadorbotoes.SetActive( value ); } }

    public void PrimeiraAtualizacaoUI( Jogador jogador ) {
        int index = GameManager.Sgt.Jogadores.IndexOf( jogador ) + 1;
        JogadorTitulo.text = $"Jogador {index}";

        BotoesBloqueados = ( jogador == GameManager.Sgt.JogadorEmEspera);
    }

    public void AtualizarValores(Jogador jogador) {
        VidaValor.text = jogador.Vida.ToString();
        ManaValor.text = jogador.Mana.ToString();
        ForcaValor.text = jogador.Forca.ToString();

        BotaoPinturaDeGuerra.interactable =
            jogador.SkillPinturaDeGuerra.PassarEmRegrasDeUso( jogador );

        BotaoEspadaAfiada.interactable =
            jogador.SkillEspadaAfiada.PassarEmRegrasDeUso( jogador );

        BotaoDescanso.interactable =
            jogador.SkillDescanso.PassarEmRegrasDeUso( jogador );
    }

    public GameObject InstanciarTextoEfeito( EfeitoSkill efeitoSkill ) {
        return Instantiate( PrefabEfeito, EfeitosParent );
    }
}
