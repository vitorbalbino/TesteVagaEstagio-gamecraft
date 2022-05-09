using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextUI = TMPro.TextMeshProUGUI;

/// <summary>
/// Mantém o registro de cada efeito das skills ao longo de sua duração.
/// </summary>
public class EfeitoSkill {
    public Skill SkillAplicada;
    public int DuracaoRestante;

    // Quando a força adicionada é uma variável em tempo de execução,
    //   o valor é lembrado aqui para ser removido no final do efeito.
    public int ForcaExtraTemporaria;

    // GameObject que agrupa os textos desta skill na HUD.
    public GameObject GrupoDescricao;
    TextUI TextoDuracao;

    public EfeitoSkill( Skill skill, Jogador jogador ) {
        SkillAplicada = skill;
        DuracaoRestante = skill.Duracao + 1; // +1 contando com o turno atual.

        GrupoDescricao = jogador.Interface.InstanciarTextoEfeito( this );
        TextUI[] TextosDescricao = GrupoDescricao.GetComponentsInChildren<TextUI>();

        TextosDescricao[0].text = SkillAplicada.Nome;
        TextoDuracao = TextosDescricao[1];
        TextoDuracao.text = DuracaoRestante.ToString();

        SkillAplicada.InicioEfeito( this, jogador );
    }

    public void Atualizar() {
        DuracaoRestante--;
        TextoDuracao.text = DuracaoRestante.ToString();
    }

    internal void EncerrarEfeito( Jogador jogador ) {
        SkillAplicada.EncerrarEfeito( this, jogador );
    }
}
