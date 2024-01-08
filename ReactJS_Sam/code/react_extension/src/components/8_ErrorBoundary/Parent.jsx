import React, { Component } from 'react'
import Child from './Child'

export default class Parent extends Component {

	state = {
		hasError:'' //用於標識子組件是否產生錯誤
	}

	//當Parent的子組件出現報錯時候，會觸發getDerivedStateFromError調用，並攜帶錯誤信息
	static getDerivedStateFromError(error){
		console.log('@@@',error);
		return {hasError:error}
	}

	componentDidCatch(){
		console.log('此處統計錯誤，反饋給服務器，用於通知編碼人員進行bug的解決');
	}

	render() {
		return (
			<div>
				<h2>我是Parent組件</h2>
				{this.state.hasError ? <h2>當前網絡不穩定，稍後再試</h2> : <Child/>}
			</div>
		)
	}
}
