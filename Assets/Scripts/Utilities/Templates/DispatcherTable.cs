using System;
using System.Collections.Generic;

namespace NXT.Templates {

    public class DispatcherTable<T> {

        private Dictionary<T, Delegate> dispatcher;

        public DispatcherTable() {  dispatcher = new Dictionary<T, Delegate>(); }
        public DispatcherTable(int size) { dispatcher = new Dictionary<T, Delegate>(size); }

        public void SetAction(T key, Delegate action) { dispatcher[key] = action; }
        public void AddAction(T key, Delegate action) {
            if(!dispatcher.ContainsKey(key)) dispatcher.Add(key, action);
            else PokemonNXT.Warn("[PNDispatcherTable] Key: " + key + " already exists in dispatcher table, if you want to upsert you may use the Set method");
        }
        public void RemoveAction(T key, Delegate action) {
            if(dispatcher.ContainsKey(key)) dispatcher.Remove(key);
            else PokemonNXT.Warn("[PNDispatcherTable] Key: " + key + " does not exist in dispatcher table, nothing was removed");
        }

        public void Dispatch(T key) {
            if(dispatcher.ContainsKey(key)) dispatcher[key].DynamicInvoke();
            else PokemonNXT.Error("[PNDispatcherTable] Action does not exists for key: " + key);
        }

        public void Dispatch<K>(T key, K arg) {
            if(dispatcher.ContainsKey(key)) dispatcher[key].DynamicInvoke(arg);
            else PokemonNXT.Error("[PNDispatcherTable]  Action does not exists for key: " + key);
        }
    }
}