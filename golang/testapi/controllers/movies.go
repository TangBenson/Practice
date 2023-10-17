package controllers

import (
	"fmt"
	"net/http"
)

/*
http.ResponseWriter 他是 net/http 中的 interface, 用來構成(construct) http response
*http.Request 他是 net/http 中的 struct, 代表 client 送出的 request, 或者 server 接到的 request

為什麼 *http.Request 要用指標的方式
1. 因為http.ResponseWriter 是一個interface，他本身的實作就是一個 pointer，意思就是說其實這裡傳的兩個參數都是指標
2. 那為什麼這裡的參數都要用指標 ？ 這意味著你可以傳遞它而不創建整個結構的副本，節省資源
*/

func AllMovies(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintln(w, "not implemented !")
}

func FindMovie(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintln(w, "not implemented !")
}

func CreateMovie(w http.ResponseWriter, t *http.Request) {
	fmt.Fprintln(w, "not implemented !")
}

func UpdateMovie(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintln(w, "not implemented !")
}

func DeleteMovie(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintln(w, "not implemented !")
}
