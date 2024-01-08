## 1. setState

### setState更新狀態的2種寫法

```
	(1). setState(stateChange, [callback])------對像式的setState
            1.stateChange為狀態改變對像(該對象可以體現出狀態的更改)
            2.callback是可選的回調函數, 它在狀態更新完畢、界面也更新後(render調用後)才被調用
					
	(2). setState(updater, [callback])------函數式的setState
            1.updater為返回stateChange對象的函數。
            2.updater可以接收到state和props。
            4.callback是可選的回調函數, 它在狀態更新、界面也更新後(render調用後)才被調用。
總結:
		1.對像式的setState是函數式的setState的簡寫方式(語法糖)
		2.使用原則：
				(1).如果新狀態不依賴於原狀態 ===> 使用對像方式
				(2).如果新狀態依賴於原狀態 ===> 使用函數方式
				(3).如果需要在setState()執行後獲取最新的狀態數據, 
					要在第二個callback函數中讀取
```



------



## 2. lazyLoad

### 路由組件的lazyLoad

```js
	//1.通過React的lazy函數配合import()函數動態加載路由組件 ===> 路由組件代碼會被分開打包
	const Login = lazy(()=>import('@/pages/Login'))
	
	//2.通過<Suspense>指定在加載得到路由打包文件前顯示一個自定義loading界面
	<Suspense fallback={<h1>loading.....</h1>}>
        <Switch>
            <Route path="/xxx" component={Xxxx}/>
            <Redirect to="/login"/>
        </Switch>
    </Suspense>
```



------



## 3. Hooks

#### 1. React Hook/Hooks是什麼?

```
(1). Hook是React 16.8.0版本增加的新特性/新語法
(2). 可以讓你在函數組件中使用 state 以及其他的 React 特性
```

#### 2. 三個常用的Hook

```
(1). State Hook: React.useState()
(2). Effect Hook: React.useEffect()
(3). Ref Hook: React.useRef()
```

#### 3. State Hook

```
(1). State Hook讓函數組件也可以有state狀態, 並進行狀態數據的讀寫操作
(2). 語法: const [xxx, setXxx] = React.useState(initValue)  
(3). useState()說明:
        參數: 第一次初始化指定的值在內部作緩存
        返回值: 包含2個元素的數組, 第1個為內部當前狀態值, 第2個為更新狀態值的函數
(4). setXxx()2種寫法:
        setXxx(newValue): 參數為非函數值, 直接指定新的狀態值, 內部用其覆蓋原來的狀態值
        setXxx(value => newValue): 參數為函數, 接收原本的狀態值, 返回新的狀態值, 內部用其覆蓋原來的狀態值
```

#### 4. Effect Hook

```
(1). Effect Hook 可以讓你在函數組件中執行副作用操作(用於模擬類組件中的生命週期鉤子)
(2). React中的副作用操作:
        發ajax請求數據獲取
        設置訂閱 / 啟動定時器
        手動更改真實DOM
(3). 語法和說明: 
        useEffect(() => { 
          // 在此可以執行任何帶副作用操作
          return () => { // 在組件卸載前執行
            // 在此做一些收尾工作, 比如清除定時器/取消訂閱等
          }
        }, [stateValue]) // 如果指定的是[], 回調函數只會在第一次render()後執行
    
(4). 可以把 useEffect Hook 看做如下三個函數的組合
        componentDidMount()
        componentDidUpdate()
    	componentWillUnmount() 
```

#### 5. Ref Hook

```
(1). Ref Hook可以在函數組件中存儲/查找組件內的標籤或任意其它數據
(2). 語法: const refContainer = useRef()
(3). 作用:保存標籤對像,功能與React.createRef()一樣
```



------



## 4. Fragment

### 使用

	<Fragment><Fragment>
	<></>

### 作用

> 可以不用必須有一個真實的DOM根標籤了



<hr/>

## 5. Context

### 理解

> 一種組件間通信方式, 常用於【祖組件】與【後代組件】間通信

### 使用

```js
1) 創建Context容器對像：
	const XxxContext = React.createContext()  
	
2) 渲染子組時，外面包裹xxxContext.Provider, 通過value屬性給後代組件傳遞數據：
	<xxxContext.Provider value={數據}>
		子組件
    </xxxContext.Provider>
    
3) 後代組件讀取數據：

	//第一種方式:僅適用於類組件 
	  static contextType = xxxContext  // 聲明接收context
	  this.context // 讀取context中的value數據
	  
	//第二種方式: 函數組件與類組件都可以
	  <xxxContext.Consumer>
	    {
	      value => ( // value就是context中的value數據
	        要顯示的內容
	      )
	    }
	  </xxxContext.Consumer>
```

### 注意

	在應用開發中一般不用context, 一般都它的封裝react插件



<hr/>


## 6. 組件優化

### Component的2個問題 

> 1. 只要執行setState(),即使不改變狀態數據, 組件也會重新render()
>
> 2. 只當前組件重新render(), 就會自動重新render子組件 ==> 效率低

### 效率高的做法

>  只有當組件的state或props數據發生改變時才重新render()

### 原因

>  Component中的shouldComponentUpdate()總是返回true

### 解決

	辦法1: 
		重寫shouldComponentUpdate()方法
		比較新舊state或props數據, 如果有變化才返回true, 如果沒有返回false
	辦法2:  
		使用PureComponent
		PureComponent重寫了shouldComponentUpdate(), 只有state或props數據有變化才返回true
		注意: 
			只是進行state和props數據的淺比較, 如果只是數據對像內部數據變了, 返回false  
			不要直接修改state數據, 而是要產生新數據
	項目中一般使用PureComponent來優化



<hr/>


## 7. render props

### 如何向組件內部動態傳入帶內容的結構(標籤)?

	Vue中: 
		使用slot技術, 也就是通過組件標籤體傳入結構  <AA><BB/></AA>
	React中:
		使用children props: 通過組件標籤體傳入結構
		使用render props: 通過組件標籤屬性傳入結構, 一般用render函數屬性

### children props

	<A>
	  <B>xxxx</B>
	</A>
	{this.props.children}
	問題: 如果B組件需要A組件內的數據, ==> 做不到 

### render props

	<A render={(data) => <C data={data}></C>}></A>
	A組件: {this.props.render(內部state數據)}
	C組件: 讀取A組件傳入的數據顯示 {this.props.data} 



<hr/>

## 8. 錯誤邊界

#### 理解：

錯誤邊界：用來捕獲後代組件錯誤，渲染出備用頁面

#### 特點：

只能捕獲後代組件生命週期產生的錯誤，不能捕獲自己組件產生的錯誤和其他組件在合成事件、定時器中產生的錯誤

##### 使用方式：

getDerivedStateFromError配合componentDidCatch

```js
// 生命週期函數，一旦後台組件報錯，就會觸發
static getDerivedStateFromError(error) {
    console.log(error);
    // 在render之前觸發
    // 返回新的state
    return {
        hasError: true,
    };
}

componentDidCatch(error, info) {
    // 統計頁面的錯誤。發送請求發送到後台去
    console.log(error, info);
}
```

## 9. 組件通信方式總結

#### 方式：

		props：
			(1).children props
			(2).render props
		消息訂閱-發佈：
			pubs-sub、event等等
		集中式管理：
			redux、dva等等
		conText:
			生產者-消費者模式

#### 組件間的關係

		父子組件：props
		兄弟組件(非嵌套組件)：消息訂閱-發佈、集中式管理
		祖孫組件(跨級組件)：消息訂閱-發佈、集中式管理、conText(用的少)

