using System.Collections.Generic;

namespace WillFromAfarDownloader
{
    public enum Gender
    {
        Male, //2 in json
        Female //1 in json
    }
   public class Voice
    {
        public string Name;
        public Gender Gender;
        public string Lang;
        public string VoiceFile;

        public Voice(string name, Gender gender, string lang, string voicefile)
        {
            Name = name;
            Gender = gender;
            Lang = lang;
            VoiceFile = voicefile;
        }
    }

   public static class Voices
    {
       public static List<Voice> VoiceList = new List<Voice>();

        public static void Load()
        {
            //Ugghh
            VoiceList.Add(new Voice("Mehdi",Gender.Male, "ar_sa", "ar_sa_hd_mehdi_22k_lf.bvcu")); 
            VoiceList.Add(new Voice("Nizar", Gender.Male, "ar_sa", "ar_sa_hd_nizar_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Salma", Gender.Female, "ar_sa", "ar_sa_hd_salma_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Leila", Gender.Female, "ar_sa", "ar_sa_hd_leila_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Laia", Gender.Female, "ca_es", "ca_es_hd_laia_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Eliska", Gender.Female, "cs_cz", "czc_hd_eliska_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Mette", Gender.Female, "da_dk", "dad_hd_mette_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Rasmus", Gender.Male, "da_dk", "dad_hd_rasmus_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Lea", Gender.Female, "de_de", "ged_lea_22k_ns.bvcu"));
            VoiceList.Add(new Voice("ClaudiaSmile", Gender.Female, "de_de", "ged_hd_claudiasmile_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Jonas", Gender.Male, "de_de", "ged_jonas_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Andreas", Gender.Male, "de_de", "ged_hd_andreas_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Claudia", Gender.Female, "de_de", "ged_hd_claudia_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Sarah", Gender.Female, "de_de", "ged_hd_sarah_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Julia", Gender.Female, "de_de", "ged_hd_julia_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Klaus", Gender.Male, "de_de", "ged_hd_klaus_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Dimitris", Gender.Male, "el_gr", "grg_hd_dimitris_22k_lf.bvcu"));
            VoiceList.Add(new Voice("DimitrisSad", Gender.Male, "el_gr", "grg_dimitrissad_22k_ns.bvcu"));
            VoiceList.Add(new Voice("DimitrisHappy", Gender.Male, "el_gr", "grg_dimitrishappy_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Lisa", Gender.Female, "en_au", "en_au_hd_lisa_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Liam", Gender.Male, "en_au", "en_au_liam_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Olivia", Gender.Female, "el_au", "en_au_olivia_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Tyler", Gender.Male, "el_au", "en_au_hd_tyler_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Rachel", Gender.Male, "en_gb", "eng_hd_rachel_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Graham", Gender.Male, "en_gb", "eng_hd_graham_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Rosie", Gender.Female, "en_gb", "eng_rosie_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Peter", Gender.Male, "en_gb", "eng_hd_peter_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Harry", Gender.Male, "en_gb", "eng_harry_22k_ns.bvcu"));
            VoiceList.Add(new Voice("QueenElizabeth", Gender.Female, "en_gb", "eng_queenelizabeth_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Lucy", Gender.Female, "en_gb", "eng_hd_lucy_22k_lf.bvcu"));
            VoiceList.Add(new Voice("PeterSad", Gender.Male, "en_gb", "eng_petersad_22k_ns.bvcu"));
            VoiceList.Add(new Voice("PeterHappy", Gender.Male, "en_gb", "eng_peterhappy_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Deepa", Gender.Female, "en_in", "en_in_hd_deepa_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Rhona", Gender.Female, "en_sct", "en_sct_hd_rhona_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Rod", Gender.Male, "en_us", "enu_hd_rod_22k_lf.bvcu"));
            VoiceList.Add(new Voice("WillOldMan", Gender.Male, "en_us", "enu_willoldman_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Tracy", Gender.Female, "en_us", "enu_hd_tracy_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Kenny", Gender.Male, "en_us", "enu_hd_kenny_22k_lf.bvcu"));
            VoiceList.Add(new Voice("WillBadGuy", Gender.Male, "en_us", "enu_willbadguy_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Micah", Gender.Male, "en_us", "enu_micah_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Ella", Gender.Female, "en_us", "enu_ella_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Saul", Gender.Male, "en_us", "enu_saul_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Valeria", Gender.Female, "en_us", "enu_valeriaenglish_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Laura", Gender.Female, "en_us", "enu_hd_laura_22k_lf.bvcu"));
            VoiceList.Add(new Voice("WillLittleCreature", Gender.Male, "en_us", "enu_willlittlecreature_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Nelly", Gender.Female, "en_us", "enu_hd_nelly_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Emilio", Gender.Male, "en_us", "enu_emilioenglish_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Will", Gender.Male, "en_us", "enu_hd_will_22k_lf.bvcu"));
            VoiceList.Add(new Voice("WillUpClose", Gender.Male, "en_us", "enu_willupclose_22k_ns.bvcu"));
            VoiceList.Add(new Voice("WillHappy", Gender.Male, "en_us", "enu_willhappy_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Sharon", Gender.Female, "en_us", "enu_hd_sharon_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Karen", Gender.Female, "en_us", "enu_karen_22k_ns.bvcu"));
            VoiceList.Add(new Voice("WillSad", Gender.Male, "en_us", "enu_willsad_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Josh", Gender.Male, "en_us", "enu_josh_22k_ns.bvcu"));
            VoiceList.Add(new Voice("WillFromAfar", Gender.Male, "en_us", "enu_willfromafar_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Ryan", Gender.Male, "en_us", "enu_hd_ryan_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Scott", Gender.Male, "en_us", "enu_scott_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Antonio", Gender.Male, "es_es", "sps_hd_antonio_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Maria", Gender.Female, "es_es", "sps_hd_maria_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Ines", Gender.Female, "es_es", "sps_hd_ines_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Valeria", Gender.Female, "es_us", "spu_valeria_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Emilio", Gender.Male, "es_us", "spu_emilio_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Rosa", Gender.Female, "es_us", "spu_hd_rosa_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Rodrigo", Gender.Male, "es_us", "spu_hd_rodrigo_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Sanna", Gender.Female, "fi_fi", "fif_hd_sanna_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Hanus", Gender.Female, "fo_fo", "fo_fo_hanus_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Hanna", Gender.Female, "fo_fo", "fo_fo_hanna_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Louise", Gender.Female, "fr_ca", "frc_hd_louise_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Manon", Gender.Female, "fr_fr", "frf_hd_manon_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Alice", Gender.Female, "fr_fr", "frf_hd_alice_22k_lf.bvcu"));
            VoiceList.Add(new Voice("AntoineHappy", Gender.Male, "fr_fr", "frf_antoinehappy_22k_ns.bvcu"));
            VoiceList.Add(new Voice("MargauxHappy", Gender.Female, "fr_fr", "frf_margauxhappy_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Julie", Gender.Female, "fr_fr", "frf_hd_julie_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Robot", Gender.Male, "fr_fr", "frf_robot_22k_ns.bvcu"));
            VoiceList.Add(new Voice("AntoineFromAfar", Gender.Male, "fr_fr", "frf_antoinefromafar_22k_ns.bvcu"));
            VoiceList.Add(new Voice("MargauxHappy", Gender.Female, "fr_fr", "frf_margauxhappy_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Bruno", Gender.Male, "fr_fr", "frf_hd_bruno_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Margaux", Gender.Female, "fr_fr", "frf_hd_margaux_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Anais", Gender.Female, "fr_fr", "frf_hd_anais_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Valentin", Gender.Male, "fr_fr", "frf_valentin_22k_ns.bvcu"));
            VoiceList.Add(new Voice("AntoineUpClose", Gender.Male, "fr_fr", "frf_antoineupclose_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Claire", Gender.Female, "fr_fr", "frf_hd_claire_22k_lf.bvcu"));
            //omg somebody plz kill me, its a half of list
            VoiceList.Add(new Voice("Antoine", Gender.Male ,"fr_fr", "frf_hd_antoine_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Elise", Gender.Female, "fr_fr", "frf_elise_22k_ns.bvcu"));
            VoiceList.Add(new Voice("AntoineSad", Gender.Male, "fr_fr", "frf_antoinesad_22k_ns.bvcu"));
            VoiceList.Add(new Voice("MargauxSad", Gender.Male, "fr_fr", "frf_margauxsad_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Kal", Gender.Male, "gb_se", "gb_se_hd_kal_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Aurora", Gender.Female, "it_it", "iti_aurora_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Fabiana", Gender.Female, "it_it", "iti_hd_fabiana_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Alessio", Gender.Male, "it_it", "iti_alessio_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Vittorio", Gender.Male, "it_it", "iti_hd_vittorio_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Chiara", Gender.Female, "it_it", "iti_hd_chiara_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Sakura", Gender.Female, "ja_jp", "ja_jp_hd_sakura_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Minji", Gender.Female, "ko_kr", "ko_kr_hd_minji_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Zoe", Gender.Female, "nl_be", "dub_hd_zoe_22k_lf.bvcu"));
            VoiceList.Add(new Voice("JeroenSad", Gender.Male, "nl_be", "dub_jeroensad_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Sofie", Gender.Female, "nl_be", "dub_hd_sofie_22k_lf.bvcu"));
            VoiceList.Add(new Voice("JeroenHappy", Gender.Male, "nl_be", "dub_jeroenhappy_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Jeroen", Gender.Male, "nl_be", "dub_hd_jeroen_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Jasmijn", Gender.Male, "nl_nl", "dun_hd_jasmijn_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Max", Gender.Male, "nl_nl", "dun_hd_max_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Daan", Gender.Male, "nl_nl", "dun_hd_daan_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Femke", Gender.Female, "nl_nl", "dun_hd_femke_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Elias", Gender.Male, "no_no", "non_elias_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Kari", Gender.Female, "no_no", "non_hd_kari_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Emilie", Gender.Female, "no_no", "non_emilie_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Bente", Gender.Female, "no_no", "non_hd_bente_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Olav", Gender.Male, "no_no", "non_hd_olav_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Ania", Gender.Female, "pl_pl", "pop_hd_ania_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Marcia", Gender.Female, "pt_br", "pob_hd_marcia_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Celia", Gender.Female, "pt_pt", "poe_hd_celia_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Alyona", Gender.Female, "ru_ru", "rur_hd_alyona_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Mia", Gender.Female, "sc_se", "sc_se_hd_mia_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Samuel", Gender.Female, "sv_fi", "sv_fi_hd_samuel_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Elin", Gender.Female, "sv_se", "sws_hd_elin_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Freja", Gender.Female, "sv_se", "sws_freja_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Erik", Gender.Male, "sv_se", "sws_hd_erik_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Filip", Gender.Male, "sv_se", "sws_filip_22k_ns.bvcu"));
            VoiceList.Add(new Voice("Emma", Gender.Female, "sv_se", "sws_hd_emma_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Emil", Gender.Male, "sv_se", "sws_hd_emil_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Ipek", Gender.Female, "tr_tr", "tut_hd_ipek_22k_lf.bvcu"));
            VoiceList.Add(new Voice("Lulu", Gender.Female, "zh_cn", "zh_cn_hd_lulu_22k_lf.bvcu"));
            //Thanks god
        }
    }
}
