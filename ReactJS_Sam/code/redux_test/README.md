## 1.求和案例_redux精簡版
		(1).去除Count組件自身的狀態
		(2).src下建立:
						-redux
							-store.js
							-count_reducer.js

		(3).store.js：
					1).引入redux中的createStore函數，創建一個store
					2).createStore調用時要傳入一個為其服務的reducer
					3).記得暴露store對像

		(4).count_reducer.js：
					1).reducer的本質是一個函數，接收：preState,action，返回加工後的狀態
					2).reducer有兩個作用：初始化狀態，加工狀態
					3).reducer被第一次調用時，是store自動觸發的，
									傳遞的preState是undefined,
									傳遞的action是:{type:'@@REDUX/INIT_a.2.b.4}

		(5).在index.js中監測store中狀態的改變，一旦發生改變重新渲染<App/>
				備註：redux只負責管理狀態，至於狀態的改變驅動著頁面的展示，要靠我們自己寫。


## 2.求和案例_redux完整版
		新增文件：
			1.count_action.js 專門用於創建action對像
			2.constant.js 放置容易寫錯的type值



## 3.求和案例_redux異步action版
		 (1).明確：延遲的動作不想交給組件自身，想交給action
		 (2).何時需要異步action：想要對狀態進行操作，但是具體的數據靠異步任務返回。
		 (3).具體編碼：
		 			1).yarn add redux-thunk，並配置在store中
		 			2).創建action的函數不再返回一般對象，而是一個函數，該函數中寫異步任務。
		 			3).異步任務有結果後，分發一個同步的action去真正操作數據。
		 (4).備註：異步action不是必須要寫的，完全可以自己等待異步任務的結果了再去分發同步action。





## 4.求和案例_react-redux基本使用
			(1).明確兩個概念：
						1).UI組件:不能使用任何redux的api，只負責頁面的呈現、交互等。
						2).容器組件：負責和redux通信，將結果交給UI組件。
			(2).如何創建一個容器組件————靠react-redux 的 connect函數
							connect(mapStateToProps,mapDispatchToProps)(UI組件)
								-mapStateToProps:映射狀態，返回值是一個對像
								-mapDispatchToProps:映射操作狀態的方法，返回值是一個對像
			(3).備註1：容器組件中的store是靠props傳進去的，而不是在容器組件中直接引入
			(4).備註2：mapDispatchToProps，也可以是一個對像


## 5.求和案例_react-redux優化
			(1).容器組件和UI組件整合一個文件
			(2).無需自己給容器組件傳遞store，給<App/>包裹一個<Provider store={store}>即可。
			(3).使用了react-redux後也不用再自己檢測redux中狀態的改變了，容器組件可以自動完成這個工作。
			(4).mapDispatchToProps也可以簡單的寫成一個對像
			(5).一個組件要和redux「打交道」要經過哪幾步？
							(1).定義好UI組件---不暴露
							(2).引入connect生成一個容器組件，並暴露，寫法如下：
									connect(
										state => ({key:value}), //映射狀態
										{key:xxxxxAction} //映射操作狀態的方法
									)(UI組件)
							(4).在UI組件中通過this.props.xxxxxxx讀取和操作狀態



## 6.求和案例_react-redux數據共享版
			(1).定義一個Pserson組件，和Count組件通過redux共享數據。
			(2).為Person組件編寫：reducer、action，配置constant常量。
			(3).重點：Person的reducer和Count的Reducer要使用combineReducers進行合併，
					合併後的總狀態是一個對象！！！
			(4).交給store的是總reducer，最後注意在組件中取出狀態的時候，記得「取到位」。

## 7.求和案例_react-redux開發者工具的使用
			(1).yarn add redux-devtools-extension
			(2).store中進行配置
					import {composeWithDevTools} from 'redux-devtools-extension'
					const store = createStore(allReducer,composeWithDevTools(applyMiddleware(thunk)))

## 8.求和案例_react-redux最終版
			(1).所有變量名字要規範，盡量觸發對象的簡寫形式。
			(2).reducers文件夾中，編寫index.js專門用於匯總並暴露所有的reducer