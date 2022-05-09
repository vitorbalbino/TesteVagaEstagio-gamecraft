using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour {
    [Header( "Valores de atributos" )]
    public int Vida = 25;
    public int Mana = 1;
    int _forca = 2;
    public int Forca {
        get { return _forca; }
        set {
            // A força sempre deve ser no mínimo 1.
            if ( value < 1 ) { _forca = 1; }
            else { _forca = value; }
        }
    }

    [Header( "Interface" )]
    public InterfaceDeJogador Interface;

    [Header( "Efeitos" )]
    public List<EfeitoSkill> EfeitosAtivos = new List<EfeitoSkill>();
    public Meditacao SkillMeditacao = new Meditacao();
    public PinturaDeGuerra SkillPinturaDeGuerra = new PinturaDeGuerra();
    public EspadaAfiada SkillEspadaAfiada = new EspadaAfiada();
    public Descanso SkillDescanso = new Descanso();

    public int CooldownPinturaDeGuerra = 0;
    public int CooldownEspadaAfiada = 0;
    public int CooldownDescanso = 0;



    void OnEnable() => GameManager.Sgt.Jogadores.Add( this );
    void Start() => Interface.PrimeiraAtualizacaoUI( this );



    public void IniciarTurno() {
        // Os jogadores recebem 1 de mana no começo de seus turnos.
        Mana++;

        // A skill meditação atualiza no começo do turno, as outras no fim, segundo suas descrições.
        foreach ( EfeitoSkill efeito in EfeitosAtivos ) {
            if ( efeito.SkillAplicada.AtualizaNoComecoDoTurno ) { efeito.Atualizar(); }
        }

        EncerrarEfeitosQueNaoDuraram();

        if ( CooldownPinturaDeGuerra != 0 ) { CooldownPinturaDeGuerra--; }
        if ( CooldownEspadaAfiada != 0 ) { CooldownEspadaAfiada--; }
        if ( CooldownDescanso != 0 ) { CooldownDescanso--; }

        Interface.BotoesBloqueados = false;

        Interface.AtualizarValores( this );
    }

    public void FinalizarTurno() {
        Interface.BotoesBloqueados = true;

        foreach ( EfeitoSkill efeito in EfeitosAtivos ) {
            if ( !efeito.SkillAplicada.AtualizaNoComecoDoTurno ) { efeito.Atualizar(); }
        }

        EncerrarEfeitosQueNaoDuraram();

        Interface.AtualizarValores( this );

        GameManager.Sgt.ContinuarCicloDeJogo();
    }



    public void Atacar() {
        GameManager.Sgt.JogadorEmEspera.Vida -= Forca;
        FinalizarTurno();
    }
    public void UsarSkillMeditacao() {
        EfeitosAtivos.Add( new EfeitoSkill( SkillMeditacao, this ) );
        FinalizarTurno();
    }
    public void UsarSkillPinturaDeGuerra() {
        EfeitosAtivos.Add( new EfeitoSkill( SkillPinturaDeGuerra, this ) );
        FinalizarTurno();
    }
    public void UsarSkillEspadaAfiada() {
        EfeitosAtivos.Add( new EfeitoSkill( SkillEspadaAfiada, this ) );
        FinalizarTurno();
    }
    public void UsarSkillDescanso() {
        EfeitosAtivos.Add( new EfeitoSkill( SkillDescanso, this ) );
        FinalizarTurno();
    }

    /// <summary>
    /// Encerra efeitos sem duração restante
    /// </summary>
    internal void EncerrarEfeitosQueNaoDuraram() {
        List<EfeitoSkill> EfeitosDurantes = new List<EfeitoSkill>( EfeitosAtivos );

        foreach ( EfeitoSkill efeito in EfeitosAtivos ) {
            if ( efeito.DuracaoRestante == 0) {
                efeito.EncerrarEfeito( this );
                EfeitosDurantes.Remove( efeito );
                Destroy( efeito.GrupoDescricao );
            }
        }

        EfeitosAtivos = EfeitosDurantes;
    }
}