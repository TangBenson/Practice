## 一、todoList案例相關知識點
		1.拆分組件、實現靜態組件，注意：className、style的寫法
		2.動態初始化列表，如何確定將數據放在哪個組件的state中？
					——某個組件使用：放在其自身的state中
					——某些組件使用：放在他們共同的父組件state中（官方稱此操作為：狀態提升）
		3.關於父子之間通信：
				1.【父組件】給【子組件】傳遞數據：通過props傳遞
				2.【子組件】給【父組件】傳遞數據：通過props傳遞，要求父提前給子傳遞一個函數
		4.注意defaultChecked 和 checked的區別，類似的還有：defaultValue 和 value
		5.狀態在哪裡，操作狀態的方法就在哪裡

## 二、github搜索案例相關知識點
		1.設計狀態時要考慮全面，例如帶有網絡請求的組件，要考慮請求失敗怎麼辦。
		2.ES6小知識點：解構賦值+重命名
					let obj = {a:{b:1}}
					const {a} = obj; //傳統解構賦值
					const {a:{b}} = obj; //連續解構賦值
					const {a:{b:value}} = obj; //連續解構賦值+重命名
		3.消息訂閱與發佈機制
					1.先訂閱，再發佈（理解：有一種隔空對話的感覺）
					2.適用於任意組件間通信
					3.要在組件的componentWillUnmount中取消訂閱
		4.fetch發送請求（關注分離的設計思想）
					try {
						const response= await fetch(`/api1/search/users2?q=${keyWord}`)
						const data = await response.json()
						console.log(data);
					} catch (error) {
						console.log('請求出錯',error);
					}
				

## 三、路由的基本使用
			1.明確好界面中的導航區、展示區
			2.導航區的a標籤改為Link標籤
						<Link to="/xxxxx">Demo</Link>
			3.展示區寫Route標籤進行路徑的匹配
						<Route path='/xxxx' component={Demo}/>
			4.<App>的最外側包裹了一個<BrowserRouter>或<HashRouter>

## 四、路由組件與一般組件
			1.寫法不同：
						一般組件：<Demo/>
						路由組件：<Route path="/demo" component={Demo}/>
			2.存放位置不同：
						一般組件：components
						路由組件：pages
			3.接收到的props不同：
						一般組件：寫組件標籤時傳遞了什麼，就能收到什麼
						路由組件：接收到三個固定的屬性
											history:
														go: ƒ go(n)
														goBack: ƒ goBack()
														goForward: ƒ goForward()
														push: ƒ push(path, state)
														replace: ƒ replace(path, state)
											location:
														pathname: "/about"
														search: ""
														state: undefined
											match:
														params: {}
														path: "/about"
														url: "/about"

## 五、NavLink與封裝NavLink
				1.NavLink可以實現路由鏈接的高亮，通過activeClassName指定樣式名

## 六、Switch的使用
				1.通常情況下，path和component是一一對應的關係。
				2.Switch可以提高路由匹配效率(單一匹配)。

## 七、解決多級路徑刷新頁面樣式丟失的問題
				1.public/index.html 中 引入樣式時不寫 ./ 寫 / （常用）
				2.public/index.html 中 引入樣式時不寫 ./ 寫 %PUBLIC_URL% （常用）
				3.使用HashRouter

## 八、路由的嚴格匹配與模糊匹配
				1.默認使用的是模糊匹配（簡單記：【輸入的路徑】必須包含要【匹配的路徑】，且順序要一致）
				2.開啟嚴格匹配：<Route exact={true} path="/about" component={About}/>
				3.嚴格匹配不要隨便開啟，需要再開，有些時候開啟會導致無法繼續匹配二級路由

## 九、Redirect的使用	
				1.一般寫在所有路由註冊的最下方，當所有路由都無法匹配時，跳轉到Redirect指定的路由
				2.具體編碼：
						<Switch>
							<Route path="/about" component={About}/>
							<Route path="/home" component={Home}/>
							<Redirect to="/about"/>
						</Switch>

## 十、嵌套路由
				1.註冊子路由時要寫上父路由的path值
				2.路由的匹配是按照註冊路由的順序進行的

## 十一、向路由組件傳遞參數
				1.params參數
							路由鏈接(攜帶參數)：<Link to='/demo/test/tom/18'}>詳情</Link>
							註冊路由(聲明接收)：<Route path="/demo/test/:name/:age" component={Test}/>
							接收參數：this.props.match.params
				2.search參數
							路由鏈接(攜帶參數)：<Link to='/demo/test?name=tom&age=18'}>詳情</Link>
							註冊路由(無需聲明，正常註冊即可)：<Route path="/demo/test" component={Test}/>
							接收參數：this.props.location.search
							備註：獲取到的search是urlencoded編碼字符串，需要借助querystring解析
				3.state參數
							路由鏈接(攜帶參數)：<Link to={{pathname:'/demo/test',state:{name:'tom',age:18}}}>詳情</Link>
							註冊路由(無需聲明，正常註冊即可)：<Route path="/demo/test" component={Test}/>
							接收參數：this.props.location.state
							備註：刷新也可以保留住參數
				


## 十二、編程式路由導航
					借助this.prosp.history對像上的API對操作路由跳轉、前進、後退
							-this.prosp.history.push()
							-this.prosp.history.replace()
							-this.prosp.history.goBack()
							-this.prosp.history.goForward()
							-this.prosp.history.go()

## 十三、BrowserRouter與HashRouter的區別
			1.底層原理不一樣：
						BrowserRouter使用的是H5的history API，不兼容IE9及以下版本。
						HashRouter使用的是URL的哈希值。
			2.path表現形式不一樣
						BrowserRouter的路徑中沒有#,例如：localhost:3000/demo/test
						HashRouter的路徑包含#,例如：localhost:3000/#/demo/test
			3.刷新後對路由state參數的影響
						(1).BrowserRouter沒有任何影響，因為state保存在history對像中。
						(2).HashRouter刷新後會導致路由state參數的丟失！！！
			4.備註：HashRouter可以用於解決一些路徑錯誤相關的問題。

## 十四、antd的按需引入+自定主題
			1.安裝依賴：yarn add react-app-rewired customize-cra babel-plugin-import less less-loader
			2.修改package.json
					....
						"scripts": {
							"start": "react-app-rewired start",
							"build": "react-app-rewired build",
							"test": "react-app-rewired test",
							"eject": "react-scripts eject"
						},
					....
			3.根目錄下創建config-overrides.js
					//配置具體的修改規則
					const { override, fixBabelImports,addLessLoader} = require('customize-cra');
					module.exports = override(
						fixBabelImports('import', {
							libraryName: 'antd',
							libraryDirectory: 'es',
							style: true,
						}),
						addLessLoader({
							lessOptions:{
								javascriptEnabled: true,
								modifyVars: { '@primary-color': 'green' },
							}
						}),
					);
				4.備註：不用在組件裡親自引入樣式了，即：import 'antd/dist/antd.css'應該刪掉