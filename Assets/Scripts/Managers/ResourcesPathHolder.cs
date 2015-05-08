namespace PokemonNXT.Managers {

    public class ResourcesPathHolder {

        #region Folder names
        private const string PREFABS = "Prefabs";

        private const string CHARACTERS = "Characters"
                                ,HUMANS = "Humans"
                                    ,TRAINERS = "Trainers"
                                ,POKEMON = "Pokemon"
                            ,EFFECTS = "Effects"
                                ,POKEMON_MOVES = "PokemonMoves"                            
                            ,ENVIRONMENT = "Environment"
                                ,BUILDINGS = "Buildings", TERRAIN = "Terrain";
        #endregion


        #region Prefab paths
        public static string PokemonPrefab(string name) {
            return PREFABS + "/" + CHARACTERS + "/" + POKEMON + "/" + name;
        }
        public static string HumanPrefab(string name) {
            return PREFABS + "/" + CHARACTERS + "/" + HUMANS + "/" + name;
        }
        public static string TrainerPrefab(string name) {
            return PREFABS + "/" + CHARACTERS + "/" + POKEMON + "/" + TRAINERS + "/" + name;
        }
        public static string PokemonMovePrefab(string name) {
            return PREFABS + "/" + EFFECTS + "/" + POKEMON_MOVES + "/" + name;
        }
        #endregion
    }
}