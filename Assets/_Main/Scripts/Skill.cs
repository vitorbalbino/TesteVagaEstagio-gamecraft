using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill {
    public string Nome;
    public int CustoDeMana;
    public int Cooldown;
    public string Descricao;
    public int Duracao;

    // Determinado pela descri��o da Skill
    public bool AtualizaNoComecoDoTurno = false;

    public virtual void InicioEfeito( EfeitoSkill efeitoSkill, Jogador jogador ) { }

    public virtual bool PassarEmRegrasDeUso( Jogador jogador ) {
        return true;
    }

    public virtual void EncerrarEfeito( EfeitoSkill efeitoSkill, Jogador jogador ) { }
}



public class Meditacao : Skill {
    public Meditacao() {
        Nome = "Medita��o";
        CustoDeMana = 0;
        Cooldown = 0;
        Descricao = "No pr�ximo turno, ganhe + 2 de mana";
        Duracao = 0;
        AtualizaNoComecoDoTurno = true;
    }

    public override void EncerrarEfeito( EfeitoSkill efeitoSkill, Jogador jogador ) {
        jogador.Mana += 2;
    }
}



public class PinturaDeGuerra : Skill {
    public PinturaDeGuerra() {
        Nome = "Pintura de Guerra";
        CustoDeMana = 1;
        Cooldown = 1;
        Descricao = "Ganhe + 2 de for�a durante 3 turnos.";
        Duracao = 3;
    }

    public override bool PassarEmRegrasDeUso( Jogador jogador ) {
        if( jogador.Mana >= CustoDeMana 
            & jogador.CooldownPinturaDeGuerra == 0) { 
            return true; 
        }
        else { return false; }
    }

    public override void InicioEfeito( EfeitoSkill efeitoSkill, Jogador jogador ) {
        jogador.Mana -= CustoDeMana;
        jogador.Forca += 2;
        jogador.CooldownPinturaDeGuerra += Cooldown;
    }

    public override void EncerrarEfeito( EfeitoSkill efeitoSkill, Jogador jogador ) {
        jogador.Forca -= 2;
    }
}



public class EspadaAfiada : Skill {
    public EspadaAfiada() {
        Nome = "Espada Afiada";
        CustoDeMana = 4;
        Cooldown = 2;
        Descricao = "Multiplique a sua for�a por 2 durante 2 turnos.";
        Duracao = 2;
    }

    public override bool PassarEmRegrasDeUso( Jogador jogador ) {
        if ( jogador.Mana > CustoDeMana
            & jogador.CooldownEspadaAfiada == 0) {
            return true;
        }
        else { return false; }
    }

    public override void InicioEfeito( EfeitoSkill efeitoSkill, Jogador jogador ) {
        jogador.Mana -= CustoDeMana;
        efeitoSkill.ForcaExtraTemporaria = jogador.Forca;
        jogador.Forca += efeitoSkill.ForcaExtraTemporaria;
        jogador.CooldownEspadaAfiada += Cooldown;
    }

    public override void EncerrarEfeito( EfeitoSkill efeitoSkill, Jogador jogador ) {
        jogador.Forca -= efeitoSkill.ForcaExtraTemporaria;
    }
}



public class Descanso : Skill {
    public Descanso() {
        Nome = "Descanso";
        CustoDeMana = 1;
        Cooldown = 3;
        Descricao = "Perca 2 de for�a e ganhe 4 de vida se tiver ao menos 2 de for�a";
        Duracao = 0;
    }

    public override bool PassarEmRegrasDeUso( Jogador jogador ) {
        if ( jogador.Mana > CustoDeMana
            & jogador.CooldownDescanso == 0 ) {
            return true;
        }
        else { return false; }
    }

    public override void InicioEfeito( EfeitoSkill efeitoSkill, Jogador jogador ) {
        jogador.Mana -= CustoDeMana;
        jogador.CooldownDescanso += Cooldown;
    }

    public override void EncerrarEfeito( EfeitoSkill efeitoSkill, Jogador jogador ) {
        jogador.Forca -= 2;
        jogador.Vida += 4;
    }
}