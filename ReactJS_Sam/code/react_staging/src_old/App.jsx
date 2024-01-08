import React, { Component } from 'react'
import { Button,DatePicker } from 'antd';
import {WechatOutlined,WeiboOutlined,SearchOutlined} from '@ant-design/icons'
const { RangePicker } = DatePicker;

export default class App extends Component {
	render() {
		return (
			<div>
				App....
				<button>點我</button>
				<Button type="primary">按鈕1</Button>
				<Button >按鈕2</Button>
				<Button type="link">按鈕3</Button>
				<Button type="primary" icon={<SearchOutlined />}>
					Search
				</Button>
				<WechatOutlined />
				<WeiboOutlined />
				<DatePicker/>
				<RangePicker/>
			</div>
		)
	}
}
