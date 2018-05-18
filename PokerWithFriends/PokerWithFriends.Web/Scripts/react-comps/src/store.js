import {createStore} from 'redux';

/*
actions always looks like: 
    {type: 'add match', task: 'create new match'}
    {type: 'delete match', : "index: 1"}
*/ 

// Note: you can not change existing objects or arrays. You must use immutible data concepts.

function reducer(state, action){
    if(state === undefined){
        return {
            matches: []
        };
    }

    if(action.type === "add match"){
        return {
            ...state, // bring in everything from previous state
            matches: [...state.matches, {task: action.task}] // add new game to state
        };
    }
    
    if(action.type === "delete match"){
        const index = action.index;
        return {
            ...state,
            matches: [...state.matches.slice(0, index), ...state.matches.slice(index + 1)]
        }
    }
}

export default createStore(reducer);