export default function matchReducer(state = [], action){
    switch(action.type){
        case 'load matches':
            return [
                ...state,
                Object.assign({}, action.matches)
            ];
        case 'download matches':
            return state;
        case 'add match':
            return [
                ...state, 
                Object.assign({}, action.matches)
            ];
        // case 'delete match': 
        //     return [
        //         ...state,
        //         Object.assign({}, )
        //     ]
            
        default: 
            return state;
        
    }
}