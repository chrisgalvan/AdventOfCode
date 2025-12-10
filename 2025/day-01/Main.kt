import java.io.File

fun main() {
    val filePath = "./day-01/src/input.txt"

    first(filePath)
    second(filePath)
}

fun first(filePath: String) {
    var pos = 50
    var counter = 0

    File(filePath).readLines().forEachIndexed { index, line ->
        val (dir, amount) = line[0] to line.substring(1).toInt()
        val calc = if (dir == 'L') pos - amount else pos + amount
        pos = calc.mod(100)

        if (pos == 0) counter++
    }

    println(counter)
}

fun second(filePath: String) {
    var pos = 50
    var counter = 0
    val d = mapOf('R' to 1, 'L' to -1)

    File(filePath).readLines().forEachIndexed { index, line ->
        val (dir, amount) = line[0] to line.substring(1).toInt()
        repeat(amount) {
            pos += d[dir] ?: 0
            if (pos.mod(100) == 0) counter++
        }
    }

    println(counter)
}
