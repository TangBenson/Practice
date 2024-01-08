import {ADD_PERSON} from '../constant'

//創建增加一個人的action動作對像
export const addPerson = personObj => ({type:ADD_PERSON,data:personObj})