import java.io.File
import java.util.PriorityQueue
import kotlin.math.pow
import kotlin.math.sqrt

data class Point(val x: Int, val y: Int, val z: Int, var group: Int? = null)

fun main() {
    val filePath = "./day-08/src/input.txt"

    partOne(filePath)
    partTwo(filePath)
}

fun partOne(filePath: String) {
    val points = getPoints(filePath)
    val distances = getAllDistances(points)
    val circuits = mutableListOf<Int>()
    var connectionsLeft = 1000

    while (connectionsLeft > 0) {
        val (_, p1, p2) = distances.poll()
        linkPoints(points, p1, p2, circuits)
        connectionsLeft--
    }

    val total = circuits
        .sortedByDescending { it }
        .take(3)
        .fold(1) { acc, i -> acc * i}

    println(total)
}

fun partTwo(filePath: String) {
    val points = getPoints(filePath)
    val distances = getAllDistances(points)
    val circuits = mutableListOf<Int>()

    while (true) {
        val (_, p1, p2) = distances.poll()
        linkPoints(points, p1, p2, circuits)

        if (circuits[points[p1].group!!] == points.size || circuits[points[p2].group!!] == points.size) {
            val total = points[p1].x.toLong() * points[p2].x.toLong()
            println(total)
            break
        }
    }
}

fun getPoints(filePath: String): List<Point> {
    return File(filePath)
        .readLines()
        .map { line ->
            val (x, y, z) =  line.split(',').map { it.toInt() }
            Point(x, y, z)
        }
}

fun getAllDistances(points: List<Point>): PriorityQueue<Triple<Double,Int,Int>> {
    val distances = PriorityQueue<Triple<Double,Int,Int>>(compareBy { it.first })

    points.forEachIndexed { index, p1 ->
        for (n in index + 1 until points.size) {
            val p2 = points[n]
            val dist = sqrt((p1.x - p2.x).toDouble().pow(2) + (p1.y - p2.y).toDouble().pow(2) + (p1.z - p2.z).toDouble().pow(2))

            distances.add(Triple<Double,Int,Int>(dist, index, n))
        }
    }

    return distances
}

fun linkPoints(points: List<Point>, p1Index: Int, p2Index: Int, circuits: MutableList<Int>) {
    val p1 = points[p1Index]
    val p2 = points[p2Index]

    when {
        // Both unassigned - create new group
        p1.group == null && p2.group == null -> {
            circuits.add(2)
            p1.group = circuits.lastIndex
            p2.group = circuits.lastIndex
        }
        // One unassigned - add to existing group
        p1.group == null -> {
            p1.group = p2.group
            circuits[p2.group!!]++
        }
        p2.group == null -> {
            p2.group = p1.group
            circuits[p1.group!!]++
        }
        // Both assigned but different groups - merge
        p1.group != p2.group -> {
            val oldGroup = p2.group!!
            circuits[p1.group!!] += circuits[p2.group!!]
            points.forEach { point ->
                if (point.group == oldGroup) {
                    point.group = p1.group
                }
            }
            circuits[oldGroup] = 0
        }
    }
}