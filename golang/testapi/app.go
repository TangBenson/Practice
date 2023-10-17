package main

import (
	"myapi/routes"
	"net/http" //golang 內建的套件，提供客戶端跟主機端的 http 方法實作
)

func main() {
	r := routes.NewRouter()

	http.ListenAndServe(":8080", r) //根據 mux.NewRouter() 起服務
}
