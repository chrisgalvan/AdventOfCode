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
	var group []string

	for scanner.Scan() {
		group = append(group, scanner.Text())

		if len(group) == 3 {
			line1 := group[0]
			line2 := group[1]
			line3 := group[2]

			fmt.Println(group)

			for i := 0; i < len(line1); i++ {
				foundLine2 := false
				foundLine3 := false

				for j := 0; j < len(line2); j++ {
					if line1[i] == line2[j] {
						foundLine2 = true
						break
					}
				}

				for k := 0; k < len(line3); k++ {
					if line1[i] == line3[k] {
						foundLine3 = true
						break
					}
				}

				if foundLine2 && foundLine3 {
					final = append(final, string(line1[i]))
					break
				}
			}

			group = make([]string, 0)
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
