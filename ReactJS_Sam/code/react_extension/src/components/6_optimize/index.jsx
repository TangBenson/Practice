import React, { PureComponent } from 'react'
import './index.css'

export default class Parent extends PureComponent {

	state = {carName:"奔馳c36",stus:['小張','小李','小王']}

	addStu = ()=>{
		/* const {stus} = this.state
		stus.unshift('小劉')
		this.setState({stus}) */

		const {stus} = this.state
		this.setState({stus:['小劉',...stus]})
	}

	changeCar = ()=>{
		//this.setState({carName:'邁巴赫'})

		const obj = this.state
		obj.carName = '邁巴赫'
		console.log(obj === this.state);
		this.setState(obj)
	}

	/* shouldComponentUpdate(nextProps,nextState){
		// console.log(this.props,this.state); //目前的props和state
		// console.log(nextProps,nextState); //接下要變化的目標props，目標state
		return !this.state.carName === nextState.carName
	} */

	render() {
		console.log('Parent---render');
		const {carName} = this.state
		return (
			<div className="parent">
				<h3>我是Parent組件</h3>
				{this.state.stus}&nbsp;
				<span>我的車名字是：{carName}</span><br/>
				<button onClick={this.changeCar}>點我換車</button>
				<button onClick={this.addStu}>添加一個小劉</button>
				<Child carName="奧拓"/>
			</div>
		)
	}
}

class Child extends PureComponent {

	/* shouldComponentUpdate(nextProps,nextState){
		console.log(this.props,this.state); //目前的props和state
		console.log(nextProps,nextState); //接下要變化的目標props，目標state
		return !this.props.carName === nextProps.carName
	} */

	render() {
		console.log('Child---render');
		return (
			<div className="child">
				<h3>我是Child組件</h3>
				<span>我接到的車是：{this.props.carName}</span>
			</div>
		)
	}
}
