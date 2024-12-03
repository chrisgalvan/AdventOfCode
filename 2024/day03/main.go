package main

import (
	"adventofcode/2024/utils"
	"bufio"
	"fmt"
	"os"
	"regexp"
)

func main() {
	file, err := os.Open("input.txt")
	if err != nil {
		panic(err)
	}

	defer file.Close()

	scanner := bufio.NewScanner(file)

	var lines []string

	for scanner.Scan() {
		lines = append(lines, scanner.Text())
	}

	one(lines)
	two(lines)
}

func one(lines []string) {
	pattern := `mul\((\d{1,3}),(\d{1,3})\)`
	re := regexp.MustCompile(pattern)
	total := 0

	for _, line := range lines {
		matches := re.FindAllStringSubmatch(line, -1)

		for _, match := range matches {
			//numbers := re2.FindAllStringSubmatch(match, -1)
			if len(match) == 3 {
				total += utils.ToInt(match[1]) * utils.ToInt(match[2])
			}
		}
	}

	fmt.Printf("One - Result %d\n", total)
}

func two(lines []string) {
	fmt.Printf("Two - Result %d\n")
}
