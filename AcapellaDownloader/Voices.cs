using System.Collections.Generic;

namespace AcapellaDownloader
{
    public enum Gender
    {
        Female,
        Male,
    }
   public class Voice
    {
        public string Name;
        public Gender Gender;
        public string Lang;
        public string VoiceId;

        public Voice(string name, Gender gender, string lang, string voiceId)
        {
            Name = name;
            Gender = gender;
            Lang = lang;
            VoiceId = voiceId;
        }
    }

   public static class Voices
    {
       public static List<Voice> VoiceList;
       static Voices()
        {
	        VoiceList = new List<Voice>
	        {
		        new Voice("Mehdi", Gender.Male, "ar_sa", "ar_sa_mehdi_22k_ns.bvcu"),
		        new Voice("Nizar", Gender.Male, "ar_sa", "ar_sa_nizar_22k_ns.bvcu"),
		        new Voice("Salma", Gender.Female, "ar_sa", "ar_sa_salma_22k_ns.bvcu"),
		        new Voice("Leila", Gender.Female, "ar_sa", "ar_sa_leila_22k_ns.bvcu"),
		        new Voice("Laia", Gender.Female, "ca_es", "ca_es_laia_22k_ns.bvcu"),
		        new Voice("Eliska", Gender.Female, "cs_cz", "czc_eliska_22k_ns.bvcu"),
		        new Voice("Mette", Gender.Female, "da_dk", "dad_mette_22k_ns.bvcu"),
		        new Voice("Rasmus", Gender.Male, "da_dk", "dad_rasmus_22k_ns.bvcu"),
		        new Voice("Lea", Gender.Female, "de_de", "ged_lea_22k_ns.bvcu"),
		        new Voice("ClaudiaSmile", Gender.Female, "de_de", "ged_claudiasmile_22k_ns.bvcu"),
		        new Voice("Jonas", Gender.Male, "de_de", "ged_jonas_22k_ns.bvcu"),
		        new Voice("Andreas", Gender.Male, "de_de", "ged_andreas_22k_ns.bvcu"),
		        new Voice("Claudia", Gender.Female, "de_de", "ged_claudia_22k_ns.bvcu"),
		        new Voice("Sarah", Gender.Female, "de_de", "ged_sarah_22k_ns.bvcu"),
		        new Voice("Julia", Gender.Female, "de_de", "ged_julia_22k_ns.bvcu"),
		        new Voice("Klaus", Gender.Male, "de_de", "ged_klaus_22k_ns.bvcu"),
		        new Voice("Dimitris", Gender.Male, "el_gr", "grg_dimitris_22k_ns.bvcu"),
		        new Voice("DimitrisSad", Gender.Male, "el_gr", "grg_dimitrissad_22k_ns.bvcu"),
		        new Voice("DimitrisHappy", Gender.Male, "el_gr", "grg_dimitrishappy_22k_ns.bvcu"),
		        new Voice("Lisa", Gender.Female, "en_au", "en_au_lisa_22k_ns.bvcu"),
		        new Voice("Liam", Gender.Male, "en_au", "en_au_liam_22k_ns.bvcu"),
		        new Voice("Olivia", Gender.Female, "en_au", "en_au_olivia_22k_ns.bvcu"),
		        new Voice("Tyler", Gender.Male, "en_au", "en_au_tyler_22k_ns.bvcu"),
		        new Voice("Rachel", Gender.Male, "en_gb", "eng_rachel_22k_ns.bvcu"),
		        new Voice("Graham", Gender.Male, "en_gb", "eng_graham_22k_ns.bvcu"),
		        new Voice("Rosie", Gender.Female, "en_gb", "eng_rosie_22k_ns.bvcu"),
		        new Voice("Peter", Gender.Male, "en_gb", "eng_peter_22k_ns.bvcu"),
		        new Voice("Harry", Gender.Male, "en_gb", "eng_harry_22k_ns.bvcu"),
		        new Voice("QueenElizabeth", Gender.Female, "en_gb", "eng_queenelizabeth_22k_ns.bvcu"),
		        new Voice("Lucy", Gender.Female, "en_gb", "eng_lucy_22k_ns.bvcu"),
		        new Voice("PeterSad", Gender.Male, "en_gb", "eng_petersad_22k_ns.bvcu"),
		        new Voice("PeterHappy", Gender.Male, "en_gb", "eng_peterhappy_22k_ns.bvcu"),
		        new Voice("Deepa", Gender.Female, "en_in", "en_in_deepa_22k_ns.bvcu"),
		        new Voice("Rhona", Gender.Female, "gd_GB", "en_sct_rhona_22k_ns.bvcu"),
		        new Voice("Rod", Gender.Male, "en_us", "enu_rod_22k_ns.bvcu"),
		        new Voice("WillOldMan", Gender.Male, "en_us", "enu_willoldman_22k_ns.bvcu"),
		        new Voice("Tracy", Gender.Female, "en_us", "enu_tracy_22k_ns.bvcu"),
		        new Voice("Kenny", Gender.Male, "en_us", "enu_kenny_22k_ns.bvcu"),
		        new Voice("WillBadGuy", Gender.Male, "en_us", "enu_willbadguy_22k_ns.bvcu"),
		        new Voice("Micah", Gender.Male, "en_us", "enu_micah_22k_ns.bvcu"),
		        new Voice("Ella", Gender.Female, "en_us", "enu_ella_22k_ns.bvcu"),
		        new Voice("Saul", Gender.Male, "en_us", "enu_saul_22k_ns.bvcu"),
		        new Voice("Valeria US", Gender.Female, "en_us", "enu_valeriaenglish_22k_ns.bvcu"),
		        new Voice("Laura", Gender.Female, "en_us", "enu_laura_22k_ns.bvcu"),
		        new Voice("WillLittleCreature", Gender.Male, "en_us", "enu_willlittlecreature_22k_ns.bvcu"),
		        new Voice("Nelly", Gender.Female, "en_us", "enu_nelly_22k_ns.bvcu"),
		        new Voice("Emilio US", Gender.Male, "en_us", "enu_emilioenglish_22k_ns.bvcu"),
		        new Voice("Will", Gender.Male, "en_us", "enu_will_22k_ns.bvcu"),
		        new Voice("WillUpClose", Gender.Male, "en_us", "enu_willupclose_22k_ns.bvcu"),
		        new Voice("WillHappy", Gender.Male, "en_us", "enu_willhappy_22k_ns.bvcu"),
		        new Voice("Sharon", Gender.Female, "en_us", "enu_sharona_22k_ns.bvcu"),
		        new Voice("Karen", Gender.Female, "en_us", "enu_karen_22k_ns.bvcu"),
		        new Voice("WillSad", Gender.Male, "en_us", "enu_willsad_22k_ns.bvcu"),
		        new Voice("Josh", Gender.Male, "en_us", "enu_josh_22k_ns.bvcu"),
		        new Voice("WillFromAfar", Gender.Male, "en_us", "enu_willfromafar_22k_ns.bvcu"),
		        new Voice("Ryan", Gender.Male, "en_us", "enu_ryan_22k_ns.bvcu"),
		        new Voice("Scott", Gender.Male, "en_us", "enu_scott_22k_ns.bvcu"),
		        new Voice("Antonio", Gender.Male, "es_es", "sps_antonio_22k_ns.bvcu"),
		        new Voice("Maria", Gender.Female, "es_es", "sps_maria_22k_ns.bvcu"),
		        new Voice("Ines", Gender.Female, "es_es", "sps_ines_22k_ns.bvcu"),
		        new Voice("Valeria Spanish", Gender.Female, "es_us", "spu_valeria_22k_ns.bvcu"),
		        new Voice("Emilio Spanish", Gender.Male, "es_us", "spu_emilio_22k_ns.bvcu"),
		        new Voice("Rosa", Gender.Female, "es_us", "spu_rosa_22k_ns.bvcu"),
		        new Voice("Rodrigo", Gender.Male, "es_us", "spu_rodrigo_22k_ns.bvcu"),
		        new Voice("Sanna", Gender.Female, "fi_fi", "fif_sanna_22k_ns.bvcu"),
		        new Voice("Hanus", Gender.Female, "fo_fo", "fo_fo_hanus_22k_ns.bvcu"),
		        new Voice("Hanna", Gender.Female, "fo_fo", "fo_fo_hanna_22k_ns.bvcu"),
		        new Voice("Louise", Gender.Female, "fr_ca", "frc_louise_22k_ns.bvcu"),
		        new Voice("Manon", Gender.Female, "fr_fr", "frf_manon_22k_ns.bvcu"),
		        new Voice("Alice", Gender.Female, "fr_fr", "frf_alice_22k_ns.bvcu"),
		        new Voice("AntoineHappy", Gender.Male, "fr_fr", "frf_antoinehappy_22k_ns.bvcu"),
		        new Voice("MargauxHappy", Gender.Female, "fr_fr", "frf_margauxhappy_22k_ns.bvcu"),
		        new Voice("Julie", Gender.Female, "fr_fr", "frf_julie_22k_ns.bvcu"),
		        new Voice("Robot", Gender.Male, "fr_fr", "frf_robot_22k_ns.bvcu"),
		        new Voice("AntoineFromAfar", Gender.Male, "fr_fr", "frf_antoinefromafar_22k_ns.bvcu"),
		        new Voice("MargauxHappy", Gender.Female, "fr_fr", "frf_margauxhappy_22k_ns.bvcu"),
		        new Voice("Bruno", Gender.Male, "fr_fr", "frf_bruno_22k_ns.bvcu"),
		        new Voice("Margaux", Gender.Female, "fr_fr", "frf_margaux_22k_ns.bvcu"),
		        new Voice("Anais", Gender.Female, "fr_fr", "frf_anais_22k_ns.bvcu"),
		        new Voice("Valentin", Gender.Male, "fr_fr", "frf_valentin_22k_ns.bvcu"),
		        new Voice("AntoineUpClose", Gender.Male, "fr_fr", "frf_antoineupclose_22k_ns.bvcu"),
		        new Voice("Claire", Gender.Female, "fr_fr", "frf_claire_22k_ns.bvcu"),
		        new Voice("Antoine", Gender.Male, "fr_fr", "frf_antoine_22k_ns.bvcu"),
		        new Voice("Elise", Gender.Female, "fr_fr", "frf_elise_22k_ns.bvcu"),
		        new Voice("AntoineSad", Gender.Male, "fr_fr", "frf_antoinesad_22k_ns.bvcu"),
		        new Voice("MargauxSad", Gender.Male, "fr_fr", "frf_margauxsad_22k_ns.bvcu"),
		        new Voice("Kal", Gender.Male, "swe", "gb_se_kal_22k_ns.bvcu"),
		        new Voice("Aurora", Gender.Female, "it_it", "iti_aurora_22k_ns.bvcu"),
		        new Voice("Fabiana", Gender.Female, "it_it", "iti_fabiana_22k_ns.bvcu"),
		        new Voice("Alessio", Gender.Male, "it_it", "iti_alessio_22k_ns.bvcu"),
		        new Voice("Vittorio", Gender.Male, "it_it", "iti_vittorio_22k_ns.bvcu"),
		        new Voice("Chiara", Gender.Female, "it_it", "iti_chiara_22k_ns.bvcu"),
		        new Voice("Sakura", Gender.Female, "ja_jp", "ja_jp_sakura_22k_ns.bvcu"),
		        new Voice("Minji", Gender.Female, "ko_kr", "ko_kr_minji_22k_ns.bvcu"),
		        new Voice("Zoe", Gender.Female, "nl_be", "dub_zoe_22k_ns.bvcu"),
		        new Voice("JeroenSad", Gender.Male, "nl_be", "dub_jeroensad_22k_ns.bvcu"),
		        new Voice("Sofie", Gender.Female, "nl_be", "dub_sofie_22k_ns.bvcu"),
		        new Voice("JeroenHappy", Gender.Male, "nl_be", "dub_jeroenhappy_22k_ns.bvcu"),
		        new Voice("Jeroen", Gender.Male, "nl_be", "dub_jeroen_22k_ns.bvcu"),
		        new Voice("Jasmijn", Gender.Male, "nl_nl", "dun_jasmijn_22k_ns.bvcu"),
		        new Voice("Max", Gender.Male, "nl_nl", "dun_max_22k_ns.bvcu"),
		        new Voice("Daan", Gender.Male, "nl_nl", "dun_daan_22k_ns.bvcu"),
		        new Voice("Femke", Gender.Female, "nl_nl", "dun_femke_22k_ns.bvcu"),
		        new Voice("Elias", Gender.Male, "no", "non_elias_22k_ns.bvcu"),
		        new Voice("Kari", Gender.Female, "no", "non_kari_22k_ns.bvcu"),
		        new Voice("Emilie", Gender.Female, "no", "non_emilie_22k_ns.bvcu"),
		        new Voice("Bente", Gender.Female, "no", "non_bente_22k_ns.bvcu"),
		        new Voice("Olav", Gender.Male, "no", "non_olav_22k_ns.bvcu"),
		        new Voice("Ania", Gender.Female, "pl_pl", "pop_ania_22k_ns.bvcu"),
		        new Voice("Marcia", Gender.Female, "pt_br", "pob_marcia_22k_ns.bvcu"),
		        new Voice("Celia", Gender.Female, "pt_pt", "poe_celia_22k_ns.bvcu"),
		        new Voice("Alyona", Gender.Female, "ru_ru", "rur_alyona_22k_ns.bvcu"),
		        new Voice("Mia", Gender.Female, "swe", "sc_se_mia_22k_ns.bvcu"),
		        new Voice("Samuel", Gender.Female, "sv_fi", "sv_fi_samuel_22k_ns.bvcu"),
		        new Voice("Elin", Gender.Female, "sv_se", "sws_elin_22k_ns.bvcu"),
		        new Voice("Freja", Gender.Female, "sv_se", "sws_freja_22k_ns.bvcu"),
		        new Voice("Erik", Gender.Male, "sv_se", "sws_erik_22k_ns.bvcu"),
		        new Voice("Filip", Gender.Male, "sv_se", "sws_filip_22k_ns.bvcu"),
		        new Voice("Emma", Gender.Female, "sv_se", "sws_emma_22k_ns.bvcu"),
		        new Voice("Emil", Gender.Male, "sv_se", "sws_emil_22k_ns.bvcu"),
		        new Voice("Ipek", Gender.Female, "tr_tr", "tut_ipek_22k_ns.bvcu"),
		        new Voice("Lulu", Gender.Female, "zh_cn", "zh_cn_lulu_22k_ns.bvcu")
	        };

        }
    }
}
