package main

import (
	"bufio"
	"os"
)

func main() {
	file, err := os.Open("test2.txt")

	if err != nil {
		println(err)
	}

	defer file.Close()

	scanner := bufio.NewScanner(file)

	for scanner.Scan() {
		line := scanner.Text()

		for i := 0; i < len(line)-4; i++ {
			mapper := make(map[rune]int)

			for j := 1; j <= 14; j++ {
				mapper[rune(line[i+j])] = j
			}

			if len(mapper) == 14 {
				println(i + 15)
				break
			}
		}
	}
}
