/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.Debugging
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Apex.Common;
    using Apex.DataStructures;
    using Apex.WorldGeometry;
    using UnityEngine;

    /// <summary>
    /// Visualization component that draws gizmos to represent the grid and show obstructed areas.
    /// </summary>
    [ExecuteInEditMode]
    [AddComponentMenu("Apex/Debugging/Grid Visualizer")]
    public class GridVisualizer : Visualizer
    {
        /// <summary>
        /// Controls how the grid is drawn
        /// </summary>
        public GridMode drawMode;

        /// <summary>
        /// Whether to draw the grids sub sections
        /// </summary>
        public bool drawSubSections;

        /// <summary>
        /// Controls whether the visualizer draws all grids in the scene or only those on the same GameObject as the visualizer.
        /// </summary>
        public bool drawAllGrids = false;

        /// <summary>
        /// The editor refresh interval, i.e. how often it refreshes to pick up changes made when in design mode.
        /// </summary>
        public float editorRefreshInterval = 1.0f;

        /// <summary>
        /// The grid lines color
        /// </summary>
        public Color gridLinesColor = new Color(135f / 255f, 135f / 255f, 135f / 255f);

        /// <summary>
        /// The obstacle color
        /// </summary>
        public Color obstacleColor = new Color(226f / 255f, 41f / 255f, 32f / 255f, 150f / 255f);

        /// <summary>
        /// The sub sections color
        /// </summary>
        public Color subSectionsColor = new Color(0f, 150f / 255f, 211f / 255f);

        /// <summary>
        /// The color of the bounds wire frame
        /// </summary>
        public Color boundsColor = Color.grey;

        private TimeSpan? _forceRefreshInterval;
        private DateTime _lastRefresh;

        /// <summary>
        /// How the grid visualization is displayed
        /// </summary>
        public enum GridMode
        {
            /// <summary>
            /// Draws the grid to represent how it is actually laid out
            /// </summary>
            Layout,

            /// <summary>
            /// Draws the grid by showing accessibility between grid cells
            /// </summary>
            Accessibility
        }

        private void OnEnable()
        {
            if (Application.isEditor && !Application.isPlaying && this.editorRefreshInterval > 0.0f)
            {
                _forceRefreshInterval = TimeSpan.FromSeconds(this.editorRefreshInterval);
                _lastRefresh = DateTime.UtcNow - _forceRefreshInterval.Value;
            }
        }

        /// <summary>
        /// Draws the actual visualization.
        /// </summary>
        protected override void DrawVisualization()
        {
            var forceRefresh = false;
            if (_forceRefreshInterval.HasValue && (DateTime.UtcNow - _lastRefresh) > _forceRefreshInterval.Value)
            {
                forceRefresh = true;
                _lastRefresh = DateTime.UtcNow;
            }

            var grids = this.drawAllGrids ? FindObjectsOfType<GridComponent>() : GetComponents<GridComponent>();

            if (grids != null)
            {
                foreach (var grid in grids)
                {
                    if (grid.enabled)
                    {
                        grid.EnsureGrid(forceRefresh, false);
                        DrawGrid(grid.grid);
                    }
                }
            }
        }

        private void DrawGrid(IGrid grid)
        {
            if (grid.sizeX == 0 || grid.sizeZ == 0 || grid.cellSize == 0)
            {
                return;
            }

            var step = grid.cellSize;
            var y = grid.origin.y + 0.05f;

            Gizmos.color = this.gridLinesColor;

            if (this.drawMode == GridMode.Layout)
            {
                for (float x = grid.left.edge; x <= grid.right.edge; x += step)
                {
                    Gizmos.DrawLine(new Vector3(x, y, grid.bottom.edge), new Vector3(x, y, grid.top.edge));
                }

                for (float z = grid.bottom.edge; z <= grid.top.edge; z += step)
                {
                    Gizmos.DrawLine(new Vector3(grid.left.edge, y, z), new Vector3(grid.right.edge, y, z));
                }
            }
            else
            {
                DrawGridHeights(grid);
            }

            foreach (var c in grid.cells)
            {
                var walkableToSomeone = c.isWalkable(AttributeMask.All);
                var walkableToEveryone = c.isWalkable(AttributeMask.None);

                if (!walkableToSomeone)
                {
                    Gizmos.color = this.obstacleColor;
                    Gizmos.DrawCube(c.position, new Vector3(step, 0.05f, step));
                }
                else if (!walkableToEveryone)
                {
                    var half = this.obstacleColor;
                    half.a = half.a * 0.5f;
                    Gizmos.color = half;
                    Gizmos.DrawCube(c.position, new Vector3(step, 0.05f, step));
                }
            }

            if (this.drawSubSections)
            {
                Gizmos.color = this.subSectionsColor;
                foreach (var section in grid.gridSections)
                {
                    var subCenter = section.bounds.center;
                    subCenter.y = y;
                    Gizmos.DrawWireCube(subCenter, section.bounds.size);
                }
            }

            Gizmos.color = this.boundsColor;
            Gizmos.DrawWireCube(grid.bounds.center, grid.bounds.size);
        }

        private void DrawGridHeights(IGrid grid)
        {
            VectorXZ[] directions = new[] { new VectorXZ(-1, 0), new VectorXZ(-1, 1), new VectorXZ(0, 1), new VectorXZ(1, 1) };
            var heightAdj = new Vector3(0.0f, 0.05f, 0.0f);
            var matrix = grid.cellMatrix;

            for (int x = 0; x < matrix.columns; x++)
            {
                for (int z = 0; z < matrix.rows; z++)
                {
                    var c = matrix[x, z];
                    if (!c.IsWalkableToAny())
                    {
                        continue;
                    }

                    var curPos = new VectorXZ(x, z);
                    for (int i = 0; i < 4; i++)
                    {
                        var checkPos = curPos + directions[i];
                        var other = matrix[checkPos.x, checkPos.z];

                        if (other != null && other.isWalkableFrom(c, AttributeMask.All))
                        {
                            Gizmos.DrawLine(c.position + heightAdj, other.position + heightAdj);
                        }
                    }
                }
            }
        }
    }
}
