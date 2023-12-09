package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

type File struct {
	Name string
	Size int
}

type Folder struct {
	Parent  *Folder
	Name    string
	Size    int
	Files   map[string]*File
	Folders map[string]*Folder
}

func main() {
	file, err := os.Open("input.txt")

	if err != nil {
		panic(err)
	}

	defer file.Close()

	scanner := bufio.NewScanner(file)

	root := &Folder{Name: "/", Parent: nil, Size: 0, Folders: map[string]*Folder{}, Files: map[string]*File{}}
	node := root

	for scanner.Scan() {
		line := scanner.Text()
		tokens := strings.Fields(line)
		// println(line)

		switch tokens[0] {
		case "$":
			// command
			if tokens[1] == "cd" {
				folderName := tokens[2]

				switch folderName {
				case "/":
					node = root
				case "..":
					node = node.Parent
				default:
					node = node.Folders[folderName]
				}
			}
		case "dir":
			// folder
			node.Folders[tokens[1]] = &Folder{Name: tokens[1], Parent: node, Size: 0, Folders: map[string]*Folder{}, Files: map[string]*File{}}
		default:
			// file
			node.Files[tokens[1]] = &File{Size: parseInt(tokens[0]), Name: tokens[1]}
			node.Size += parseInt(tokens[0])
			var parentNode = node.Parent
			for parentNode != nil {
				parentNode.Size += parseInt(tokens[0])
				parentNode = parentNode.Parent
			}
		}
	}

	list := make(map[string]int)
	traverseFolders(root, &list)

	total := 0
	for el, i := range list {
		fmt.Printf("folder %s: %d size\n", el, i)
		total += i
	}
	println(total)
}

func parseInt(val string) int {
	v, _ := strconv.ParseInt(val, 10, 64)
	return int(v)
}

func traverseFolders(node *Folder, list *map[string]int) {
	for _, f := range node.Folders {
		traverseFolders(f, list)

		if 100000 >= f.Size {
			(*list)[f.Name+"-"+f.Parent.Name] = f.Size
		}
	}
}
