import * as axios from 'axios';


export function getAllUsers(){
    return axios.get("http://localhost:56035/api/users");
}

export function getUserById(id){
    return axios.get("http://localhost:56035/api/users/" + id);
}

export function createNewUser(data){
    return axios.post("http://localhost:56035/api/users", data);
}

export function updateUser(data){
    return axios.put("http://localhost:56035/api/users/" + data.id, data);
}

export function deleteUser(id){
    return axios.delete("http://localhost:56035/api/users/" + id);
}