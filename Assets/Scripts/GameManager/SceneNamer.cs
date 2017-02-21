using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Developed by: Higor (hcmb)
/// Contains dictionaries for parsing scene names from unity format to UI displayable
/// </summary>
public class SceneNamer : MonoBehaviour
{

    static Dictionary<string, string> ui = new Dictionary<string, string>();

    public void Start()
    {
        /* Menu */
        ui.Add("GameOpening", "Main Menu");
        ui.Add("ChooseCharacter", "Escolha de Personagem");
        /* Cin */
        ui.Add("Cin", "CIn");
        ui.Add("TeacherRoomBig_1", "Sala de Professor");
        ui.Add("TeacherRoomBig_2", "Sala de Professor");
        ui.Add("TeacherRoomBig_3", "Sala de Professor");
        ui.Add("TeacherRoomBig_4", "Sala de Professor");
        ui.Add("TeacherRoomBig_5", "Sala de Professor");
        ui.Add("TeacherRoomBig_6", "Sala de Professor");
        ui.Add("TeacherRoomBig_7", "Sala de Professor");
        ui.Add("TeacherRoomBig_8", "Sala de Professor");
        ui.Add("TeacherRoomBig_9", "Sala de Professor");
        ui.Add("TeacherRoomBig_10", "Sala de Professor");
        ui.Add("TeacherRoomBig_11", "Sala de Professor");
        ui.Add("TeacherRoomSmall_1", "Sala de Professor");
        ui.Add("TeacherRoomSmall_2", "Sala de Professor");
        ui.Add("TeacherRoomSmall_3", "Sala de Professor");
        ui.Add("TeacherRoomSmall_4", "Sala de Professor");
        ui.Add("TeacherRoomSmall_5", "Sala de Professor");
        ui.Add("TeacherRoomSmall_6", "Sala de Professor");
        ui.Add("TeacherRoomSmall_7", "Sala de Professor");
        ui.Add("TeacherRoomSmall_8", "Sala de Professor");
        ui.Add("TeacherRoomSmall_9", "Sala de Professor");
        ui.Add("TeacherRoomSmall_10", "Sala de Professor");
        ui.Add("TeacherRoomSmall_11", "Sala de Professor");
        ui.Add("TeacherRoomSmall_12", "Sala de Professor");
        ui.Add("Grad_1", "Grad 1");
        ui.Add("Grad_2", "Grad 2");
        ui.Add("Grad_3", "Grad 3");
        ui.Add("Grad_4", "Grad 4");
        ui.Add("Grad_5", "Grad 5");
        ui.Add("ResearchLab_1", "Laboratório de Pesquisa");
        ui.Add("ResearchLab_2", "Laboratório de Pesquisa");
        ui.Add("BathroomF_1", "Banheiro Feminino");
        ui.Add("BathroomF_2", "Banheiro Feminino");
		ui.Add("BathroomF_3", "Banheiro Feminino");
        ui.Add("BathroomM_1", "Banheiro Masculino");
        ui.Add("BathroomM_2", "Banheiro Masculino");
		ui.Add("BathroomM_3", "Banheiro Masculino");
        ui.Add("Clasroom_1", "Sala de Aula");
        ui.Add("Clasroom_2", "Sala de Aula");
		ui.Add("Clasroom_3", "Sala de Aula");
        ui.Add("LivingRoom", "Sala de Convivência");
        ui.Add("Anfitheater", "Anfiteatro");
        ui.Add("Pet", "PET");
        ui.Add("Citi", "CITI");
        ui.Add("Voxar", "VOXAR");
        ui.Add("Kitchen", "Copa");
        /* CinParking */
        ui.Add("CinParking", "Estacionamento");
        /* CCEN */
        ui.Add("CCEN", "CCEN");
		ui.Add("CCENUpstairs", "CCEN - 2º Andar");
		ui.Add("SecGrad", "SecGrad");
		ui.Add("Library", "Biblioteca");
		/* AREA 2 */
		ui.Add("Area2Path", "Caminho para Área 2");
    }

    /// <summary>
    /// Converts a formal scene name to an agradable name for displaying for player in UI components
    /// </summary>
    /// <param name="scene">The formal scene name for converting</param>
    /// <returns></returns>
    public static string UI(string scene)
    {
        if (ui.ContainsKey(scene))
            return ui[scene];
        return scene;
    }
}
