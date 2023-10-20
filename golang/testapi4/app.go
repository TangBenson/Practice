package main

import (
	"net/http"

	"github.com/gin-gonic/gin"
)

type TestData struct {
	Hello string `json:"hello"`
}
type Test struct {
	Name string `json:"name"`
}

func test(c *gin.Context) {
	data := new(TestData)
	data.Hello = "world!"
	c.JSON(http.StatusOK, data)
}

func main() {
	/*
		gin FrameWork:
		gin 是一个用 Go 编写的 HTTP server 框架
		gin 是一套用 golang 原生的 net/http package 封裝過後的框架，效能完全有保證，還有各種方便的 data binding 機制
		gin.Context 裡面包含有 request 以及 response 的內容與操作，並且支援直接回傳 JSON 自動進行轉換

		參考:https://bingdoal.github.io/backend/2022/03/golang-gin-gonic-intro/
	*/

	server := gin.Default()
	//設定路由
	apiV1 := server.Group("api/v1/")

	//api url:http://localhost:8080/api/v1/hello/xxx
	apiV1.GET("hello/:name", func(c *gin.Context) {
		name := c.Param("name")
		c.JSON(http.StatusOK, gin.H{"hello": name})
	})
	//api url:http://localhost:8080/api/v1/hello?name=xxx
	apiV1.GET("hello", func(c *gin.Context) {
		name := c.Query("name")
		c.JSON(http.StatusOK, gin.H{"hello": name})
	})
	//api url:http://localhost:8080/api/v1/hello，body要用json定義name參數
	apiV1.POST("hello", func(c *gin.Context) {
		body := map[string]string{}
		//body := new(Test)
		c.BindJSON(&body)
		c.JSON(http.StatusOK, gin.H{"hello": body["name"]})
		// c.JSON(http.StatusOK, gin.H{"hello": body.Name})
	})
	//api url:http://localhost:8080/api/v1/hello/form，body要用Form data定義name參數
	apiV1.POST("hello/form", func(c *gin.Context) {
		name := c.PostForm("name")
		c.JSON(http.StatusOK, gin.H{"hello": name})
	})
	server.Run(":8080")
}
