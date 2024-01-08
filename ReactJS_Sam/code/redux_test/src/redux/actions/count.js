/* 
	該文件專門為Count組件生成action對像
*/
import {INCREMENT,DECREMENT} from '../constant'

//同步action，就是指action的值為Object類型的一般對像
export const increment = data => ({type:INCREMENT,data})
export const decrement = data => ({type:DECREMENT,data})

//異步action，就是指action的值為函數,異步action中一般都會調用同步action，異步action不是必須要用的。
export const incrementAsync = (data,time) => {
	return (dispatch)=>{
		setTimeout(()=>{
			dispatch(increment(data))
		},time)
	}
}