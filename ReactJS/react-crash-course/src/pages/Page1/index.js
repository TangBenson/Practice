import A from './components/A'
import B from './components/B'
import './index.css'
import { useState } from 'react'

const app = {
    color: 'red'
}

const Page1 = () => {
    const [a, setA] = useState(100)
    function plus() {
        // setA(a + 200) //不是好方法
        setA(function (prev){
            return prev +200
        })
    }
    return (
        <div>
            <div style={app}>dsvdsvdsv</div>
            {a}
            <button onClick={plus}>加法</button>
            <div className='app'>
                <A />
                <B />
            </div>
        </div>
    )
}

export default Page1