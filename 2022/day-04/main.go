package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

func main() {
	file, err := os.Open("input.txt")

	if err != nil {
		fmt.Println(err)
	}

	scanner := bufio.NewScanner(file)

	defer file.Close()

	count1 := 0
	count2 := 0

	for scanner.Scan() {
		line := strings.Split(scanner.Text(), ",")

		pair1 := strings.Split(line[0], "-")
		pair2 := strings.Split(line[1], "-")

		if cint(pair1[0]) >= cint(pair2[0]) && cint(pair1[1]) <= cint(pair2[1]) ||
			cint(pair2[0]) >= cint(pair1[0]) && cint(pair2[1]) <= cint(pair1[1]) {
			count1++
		}

		if cint(pair1[0]) >= cint(pair2[0]) && cint(pair1[0]) <= cint(pair2[1]) ||
			cint(pair1[1]) >= cint(pair2[0]) && cint(pair1[1]) <= cint(pair2[1]) ||
			cint(pair2[0]) >= cint(pair1[0]) && cint(pair2[0]) <= cint(pair1[1]) ||
			cint(pair2[1]) >= cint(pair1[0]) && cint(pair2[1]) <= cint(pair1[1]) {
			count2++
		}
	}

	println(count1)
	println(count2)
}

func cint(input string) int {
	n, _ := strconv.Atoi(input)

	return n
}
