package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
)

func main() {
	file, err := os.Open("input.txt")

	if err != nil {
		fmt.Println(err)
	}

	defer file.Close()

	scanner := bufio.NewScanner(file)

	var final []string

	for scanner.Scan() {
		line := scanner.Text()

		var compartmentSize = len(line) / 2
		itemTypes := make(map[string]struct{})

		for i := 0; i < compartmentSize; i++ {
			for j := compartmentSize; j < len(line); j++ {

				if line[i] == line[j] {
					_, exists := itemTypes[string(line[i])]

					if !exists {
						itemTypes[string(line[i])] = struct{}{}
					}
				}
			}
		}

		for key, _ := range itemTypes {
			final = append(final, key)
		}
	}

	fmt.Println(final)

	r := []rune(strings.Join(final, ""))
	total := 0

	for _, el := range r {
		var ascii = int(el)
		var adjust = 38 // A = 65 ASCII

		if ascii > 91 {
			adjust = 96 // a = 97 ASCII
		}

		val := int(el) - adjust
		total += val

		fmt.Printf("%d (%c)\n", val, el)
	}

	fmt.Println(total)
}
