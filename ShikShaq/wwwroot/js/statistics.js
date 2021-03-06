﻿//Product orders ajax from api controller
$.ajax({
    method: 'get',
    url: '/api/statisticsapi/productorders',
    data: "{}",
    success: function (data) {
        // set the dimensions and margins of the graph
        var width = 600
        height = 600
        margin = 100

        // The radius of the pieplot is half the width or half the height (smallest one). I subtract a bit of margin.
        var radius = Math.min(width, height) / 2 - margin

        // append the svg object to the div called 'my_dataviz'
        var svg = d3.select("#productorders")
            .append("svg")
            .attr("width", width)
            .attr("height", height)
            .append("g")
            .attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");

        // set the color scale
        var color = d3.scaleOrdinal()
            .domain(["a", "b", "c", "d", "e", "f", "g", "h"])
            .range(d3.schemeDark2);

        // Compute the position of each group on the pie:
        var pie = d3.pie()
            .sort(null) // Do not sort group by size
            .value(function (d) { return d.value; })
        var data_ready = pie(d3.entries(data))
        // Now I know that group A goes from 0 degrees to x degrees and so on.

        // shape helper to build arcs:
        var arc = d3.arc()
            .innerRadius(radius * 0.5)         // This is the size of the donut hole
            .outerRadius(radius * 0.8)
        var outerArc = d3.arc()
            .innerRadius(radius * 0.9)
            .outerRadius(radius * 0.9)
        // Build the pie chart: Basically, each part of the pie is a path that we build using the arc function.
        svg
            .selectAll('allSlices')
            .data(data_ready)
            .enter()
            .append('path')
            .attr('d', arc)
            .attr('fill', function (d) { return (color(d.data.key)) })
            .attr("stroke", "white")
            .style("stroke-width", "2px")
            .style("opacity", 0.7)
        // Added Black Labels
        // Now add the annotation. Use the centroid method to get the best coordinates
        svg
            .selectAll('allPolylines')
            .data(data_ready)
            .enter()
            .append('polyline')
            .attr("stroke", "black")
            .style("fill", "none")
            .attr("stroke-width", 1)
            .attr('points', function (d) {
                var posA = arc.centroid(d) // line insertion in the slice
                var posB = outerArc.centroid(d) // line break: we use the other arc generator that has been built only for that
                var posC = outerArc.centroid(d); // Label position = almost the same as posB
                var midangle = d.startAngle + (d.endAngle - d.startAngle) / 2 // we need the angle to see if the X position will be at the extreme right or extreme left
                posC[0] = radius * 0.95 * (midangle < Math.PI ? 1 : -1); // multiply by 1 or -1 to put it on the right or on the left
                return [posA, posB, posC]
            })
        // Add the polylines between chart and labels:
        // The small black line connecting the chart and the text
        svg
            .selectAll('allLabels')
            .data(data_ready)
            .enter()
            .append('text')
            .text(function (d) { console.log(d.data.key); return d.data.key })
            .attr('transform', function (d) {
                var pos = outerArc.centroid(d);
                var midangle = d.startAngle + (d.endAngle - d.startAngle) / 2
                pos[0] = radius * 0.99 * (midangle < Math.PI ? 1 : -1);
                return 'translate(' + pos + ')';
            })
            .style('text-anchor', function (d) {
                var midangle = d.startAngle + (d.endAngle - d.startAngle) / 2
                return (midangle < Math.PI ? 'start' : 'end')
            })
    },
    error: function (result) {
    }
});
var _data;
var labels;
var dataset;
//Product orders ajax from api controller
$.ajax({
    method: 'get',
    url: '/api/statisticsapi/populartags',
    data: "{}",
    success: function (data) {
        _data = data
        labels = Object.keys(data);
        dataset = Object.values(data);
        // set the dimensions and margins of the graph
        var margin = { top: 30, right: 30, bottom: 70, left: 60 },
            width = 460 - margin.left - margin.right,
            height = 400 - margin.top - margin.bottom;

        // append the svg object to the body of the page
        var svg = d3.select("#populartags")
            .append("svg")
            .attr("width", width + margin.left + margin.right)
            .attr("height", height + margin.top + margin.bottom)
            .append("g")
            .attr("transform",
                "translate(" + margin.left + "," + margin.top + ")");

        // Parse the Data


            // X axis
            var x = d3.scaleBand()
                .range([0, width])
                .domain(labels)
                .padding(0.2);
            svg.append("g")
                .attr("transform", "translate(0," + height + ")")
                .call(d3.axisBottom(x))
                .selectAll("text")
                .attr("transform", "translate(-10,0)rotate(-45)")
                .style("text-anchor", "end");

            // Add Y axis
            var y = d3.scaleLinear()
                .domain([0, 10])
                .range([height, 0]);
            svg.append("g")
                .call(d3.axisLeft(y));

            // Bars
            svg.selectAll("mybar")
                .data(labels)
                .enter()
                .append("rect")
                .attr("x", function (d) { return x(d);})
                .attr("y", function (d) { console.log(d); return y(data[d]); })
                .attr("width", x.bandwidth()/2)
                .attr("height", function (d) { return (data[d]*30); })
                .attr("fill", "#69b3a2")

        
    },
    error: function (result) {
    }
});
var cart_data;
var cartlabels;
var cartdataset;
$.ajax({
    method: 'get',
    url: '/api/statisticsapi/mostincart',
    data: "{}",
    success: function (data) {
        cart_data = data
        cartlabels = Object.keys(data);
        cartdataset = Object.values(data);
        // set the dimensions and margins of the graph
        var width = 450
            height = 450
            margin = 40
        var radius = Math.min(width, height) / 2 - margin

        // append the svg object to the body of the page
        var svg = d3.select("#mostincart")
            .append("svg")
            .attr("width", width)
            .attr("height", height)
            .append("g")
            .attr("transform","translate(" + width / 2 + "," + height / 2 + ")");


        var color = d3.scaleOrdinal()
        .domain(["a", "b", "c", "d", "e", "f", "g", "h"])
        .range(d3.schemeSet2);
        
        // Compute the position of each group on the pie:
        var pie = d3.pie()
        .value(function(d) {return d.value; })
        var data_ready = pie(d3.entries(data))
       
        // shape helper to build arcs:
        var arcGenerator = d3.arc()
          .innerRadius(0)
          .outerRadius(radius)
        
        // Build the pie chart: Basically, each part of the pie is a path that we build using the arc function.
        svg
          .selectAll('mySlices')
          .data(data_ready)
          .enter()
          .append('path')
            .attr('d', arcGenerator)
            .attr('fill', function(d){ return(color(d.data.key)) })
            .attr("stroke", "black")
            .style("stroke-width", "2px")
            .style("opacity", 0.7)
         // Now add the annotation. Use the centroid method to get the best coordinates
          svg
            .selectAll('mySlices')
            .data(data_ready)
            .enter()
            .append('text')
            .text(function(d){ return d.data.key})
            .attr("transform", function(d) { return "translate(" + arcGenerator.centroid(d) + ")";  })
            .style("text-anchor", "middle")
            .style("font-size", 17)

        
    },
    error: function (result) {
    }
});