/* 
	該文件用於匯總所有的reducer為一個總的reducer
*/
//引入combineReducers，用於匯總多個reducer
import {combineReducers} from 'redux'
//引入為Count組件服務的reducer
import count from './count'
//引入為Person組件服務的reducer
import persons from './person'

//匯總所有的reducer變為一個總的reducer
export default  combineReducers({
	count,
	persons
})
