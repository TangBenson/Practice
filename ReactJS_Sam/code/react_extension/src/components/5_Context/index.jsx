import React, { Component } from 'react'
import './index.css'

//創建Context對像
const MyContext = React.createContext()
const {Provider,Consumer} = MyContext
export default class A extends Component {

	state = {username:'tom',age:18}

	render() {
		const {username,age} = this.state
		return (
			<div className="parent">
				<h3>我是A組件</h3>
				<h4>我的用戶名是:{username}</h4>
				<Provider value={{username,age}}>
					<B/>
				</Provider>
			</div>
		)
	}
}

class B extends Component {
	render() {
		return (
			<div className="child">
				<h3>我是B組件</h3>
				<C/>
			</div>
		)
	}
}

/* class C extends Component {
	//聲明接收context
	static contextType = MyContext
	render() {
		const {username,age} = this.context
		return (
			<div className="grand">
				<h3>我是C組件</h3>
				<h4>我從A組件接收到的用戶名:{username},年齡是{age}</h4>
			</div>
		)
	}
} */

function C(){
	return (
		<div className="grand">
			<h3>我是C組件</h3>
			<h4>我從A組件接收到的用戶名:
			<Consumer>
				{value => `${value.username},年齡是${value.age}`}
			</Consumer>
			</h4>
		</div>
	)
}