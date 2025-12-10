import java.io.File

fun main() {
    val filePath = "./day-03/src/input.txt"

    partOne(filePath)
    partTwo(filePath,12)
}

fun partOne(filePath: String) {
    var counter: Long = 0
    File(filePath)
        .readLines()
        .forEach { line ->

            var d1: Int = line[0].digitToInt()
            var d2: Int = line[1].digitToInt()

            for (n in 1..line.length - 1) {
                if (line[n].digitToInt() > d1 && n < line.length - 1) {
                    d1 = line[n].digitToInt()
                    d2 = line[n + 1].digitToInt()
                } else if (line[n].digitToInt() > d2) {
                    d2 = line[n].digitToInt()
                }
            }

            println("$d1 $d2")
            counter += (d1 * 10) + d2
        }

    println(counter)
}

fun partTwo(filePath: String, digits: Int) {
    var counter: Long = 0

    File(filePath)
        .readLines()
        .forEach { line ->
            var stack = ArrayDeque<Char>()
            var pos = 0

            for (n in 1..digits) {
                for (d in pos..line.length - (digits - stack.size)) {
                    if (line[d].digitToInt() > line[pos].digitToInt()) {
                        pos = d
                    }
                }

                stack.addLast(line[pos])
                pos++
            }
            counter += stack.joinToString("").toLong()
        }

    println(counter)
}