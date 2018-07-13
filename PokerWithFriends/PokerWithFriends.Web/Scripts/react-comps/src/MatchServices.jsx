import axios from 'axios';

export function getMatchesByUserId(id){
    return axios.get("http://localhost:56035/api/users/" + id + "/matches");
}

export function createMatch(match){
    return axios.post("http://localhost:56035/api/matches", match);
}

export function deleteMatch(id){
    return axios.delete("http://localhost:56035/api/matches/" + id);
}