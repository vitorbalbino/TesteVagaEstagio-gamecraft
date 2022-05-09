using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TextUI = TMPro.TextMeshProUGUI;

public class GameManager : MonoBehaviour {
    public static GameManager Sgt;

    [Header("Ordem de jogadores")]
    public List<Jogador> Jogadores = new List<Jogador>();
    public int IndexJogadorEmTurno = 0;
    public Jogador JogadorEmTurno { get { return Jogadores[IndexJogadorEmTurno]; } }
    int IndexJogadorEmEspera = 1;
    public Jogador JogadorEmEspera { get { return Jogadores[IndexJogadorEmEspera]; } }


    [Header( "UI" )]
    [SerializeField] TextUI MensagemJogadorEmTurno;
    int CicloAtual = 1;
    [SerializeField] TextUI MensagemCicloAtual;
    [SerializeField] GameObject PainelFimDeJogo;
    [SerializeField] TextUI MensagemFimDeJogo;



    void Awake() => Sgt = this;
    void Start() => JogadorEmTurno.IniciarTurno();



    /// <summary>
    /// O jogador chama este método sempre que finaliza seu turno.
    /// </summary>
    public void ContinuarCicloDeJogo() {

        if ( JogadorEmEspera.Vida <= 0 ) {
            FimDeJogo(); 
        }
        else {
            // Troca de turnos
            IndexJogadorEmEspera = IndexJogadorEmTurno;

            if ( IndexJogadorEmTurno == 1 ) {
                IndexJogadorEmTurno = 0;

                CicloAtual++;
                MensagemCicloAtual.text = CicloAtual.ToString();
            }
            else { IndexJogadorEmTurno = 1; }

            MensagemJogadorEmTurno.text = (IndexJogadorEmTurno +1 ).ToString();

            JogadorEmTurno.IniciarTurno();
        }
    }



    void FimDeJogo() { 
        PainelFimDeJogo.SetActive( true );
        MensagemFimDeJogo.text = $"Jogador {IndexJogadorEmTurno + 1} ganhou!";
    }

    public void RecomecarJogo() => SceneManager.LoadScene( 0 );
}
