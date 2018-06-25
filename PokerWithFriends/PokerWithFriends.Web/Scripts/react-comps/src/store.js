import {createStore} from 'redux';

/*
actions always looks like: 
    {type: 'load matches', task: 'load all current users matches into store'}
    {type: 'add match', task: 'create new match'}
    {type: 'delete match', : "index: 1"}
*/ 

// Note: you can not change existing objects or arrays. You must use immutible data concepts.

function reducer(store, action){
    if(!store){
        return {
            matches: []
        };
    }

    if(action.type === "add match" || action.type === "load matches"){
        return {
            ...store, // bring in everything from previous state
            matches: [...store.matches, {task: action.task}] // add new game to state
        };
    }
    
    if(action.type === "delete match"){
        const index = action.index;
        return {
            ...store,
            matches: [...store.matches.slice(0, index), ...store.matches.slice(index + 1)]
        }
    }
}

export default createStore(reducer);