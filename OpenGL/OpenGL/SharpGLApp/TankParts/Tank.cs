using System;
using System.Drawing;
using System.Numerics;
using SharpGL.Enumerations;

namespace SharpGL.TankParts
{
    public static class Tank
    {
        private const int Roundness = 25;

        // основание
        private const float BaseLength = 23;
        private const float BaseHeight = 3.5f;
        private const float BaseWidth = 10;

        // передине и задние наклонные (бампер)
        private const float BumperLength = 3f;
        private const float BumperMiddleHeight = BaseHeight / 2f;

        // крылья
        private const float FrontWingsLength = 1;
        private const float FrontWingsHeight = 1.9f;
        private const float WingsWidth = 3;

        // кабина (середина)
        private const float CabinetBottomWidth = BaseWidth + 4;
        private const float CabinetTopWidth = BaseWidth;
        private const float CabinetHeight = 3;
        private const float CabinetSlopeLength = 5;

        // основа башни (круглая часть под башней)
        private const float TurretBaseRadius = CabinetTopWidth / 2 - 2;
        private const float TurretBaseHeight = 0.5f;

        // башня
        private const float TurretHeight = 2f;
        private const float TurretLength = 4;
        private const float TurretBottomMiddleWidth = CabinetTopWidth - 2;
        private const float TurretTopMiddleWidth = TurretBottomMiddleWidth - 2.5f;
        private const float TurretTopFrontWidth = TurretTopMiddleWidth - 2;
        private const float TurretBottomFrontWidth = TurretTopFrontWidth + 2f;
        private const float TurretMiddleWidth = 0.5f;

        //основание ствола
        private const float BarrelBaseLength = 1.5f;
        private const float BarrelBaseWidth = 1.5f;

        private const float BarrelBaseHeight = 2f;

        // дуло
        private const float BarrelLength = 9;
        private const float BarrelRadius = 0.25f;

        // колеса
        private const float WheelPartWidth = .5f;
        private const float DistanceBetweenWheels = .5f;
        private const float SmallWheelHeight = .5f;
        private const int BigWheelsCount = 5;
        private const float WheelConnectorWidth = .5f;

        // большое колесо
        private const float BigWheelOuterRadius = 1.8f;
        private const float BigWheelInnerRadius = BigWheelOuterRadius * .85f;
        private const float BigWheelInnerPartRadius1 = BigWheelOuterRadius * .3f;
        private const float BigWheelInnerPartRadius2 = BigWheelInnerPartRadius1 * .8f;
        private const float BigWheelInnerPartWidth = .1f;
        private const float BigWheelConnectorRadius = BigWheelInnerPartRadius1;

        // маленькое колесо
        private const float SmallWheelOuterRadius = BigWheelOuterRadius * .8f;
        private const float SmallWheelInnerRadius = SmallWheelOuterRadius * .9f;
        private const float SmallWheelRingRadius1 = SmallWheelOuterRadius * .25f;
        private const float SmallWheelRingRadius2 = SmallWheelRingRadius1 * .8f;
        private const float SmallWheelRingWidth = .1f;

        // гусеницы
        private const float TrackUnitWidth = WheelPartWidth * 2 + WheelConnectorWidth;
        private const float TrackUnitLength = .4f;
        private const float TrackUnitThickness = .1f;
        private const float TrackUnitConnectorLength = .15f;
        private const float TrackUnitConnectorWidth = TrackUnitWidth / 9;
        private const float TrackUnitsGap = TrackUnitConnectorLength * .5f;

        public static void Draw(OpenGL gl)
        {
            // Основа
            DrawBase(gl);

            // Кабина (середина)
            DrawCabinet(gl);

            // башня
            DrawTurret(gl);

            // ходовая часть
            DrawRunningGear(gl);
        }

        private static void DrawBase(OpenGL gl)
        {
            // Основа (низ)
            gl.Color(Color.DarkGreen);
            gl.DrawParallelepiped(new Vector3(BaseLength, BaseHeight, BaseWidth));

            // угловой бампер
            gl.DoTranslate(Vector3.UnitX * BaseLength, true);
            gl.SetColor(Color.Green);

            var plateAngle = MathF.Atan2(BumperMiddleHeight, BumperLength) / MathF.PI * 180;
            gl.Repeat(() =>
            {
                //верх и ниж пластины
                gl.Repeat(() =>
                {
                    gl.Draw(BeginMode.Polygon, () =>
                    {
                        gl.Vertex(Vector3.Zero);
                        gl.Vertex(BumperLength, BumperMiddleHeight);
                        gl.Vertex(BumperLength, BumperMiddleHeight, BaseWidth);
                        gl.Vertex(Vector3.UnitZ * BaseWidth);
                    });
                }, 2, Vector3.UnitY * BaseHeight, Vector3.UnitZ * -plateAngle * 2);

                // стенки
                gl.Repeat(() =>
                {
                    gl.Draw(BeginMode.Triangles, () =>
                    {
                        gl.Vertex(Vector3.Zero);
                        gl.Vertex(BumperLength, BumperMiddleHeight);
                        gl.Vertex(Vector3.UnitY * BaseHeight);
                    });
                }, 2, Vector3.UnitZ * BaseWidth, Vector3.Zero);
            }, 2, new Vector3(-BaseLength, 0, BaseWidth), Vector3.UnitY * 180);

            // крылья
            gl.Translate(BumperLength, BaseHeight, 0);

            var zDiff = 0f;
            gl.Repeat(() =>
            {
                // верхняя пластина
                gl.Draw(BeginMode.Polygon, () =>
                {
                    gl.Vertex(Vector3.Zero);
                    gl.Vertex(-(BumperLength + BaseLength), 0);
                    gl.Vertex(-(BumperLength + BaseLength), 0, -WingsWidth);
                    gl.Vertex(0, 0, -WingsWidth);
                });

                // передняя наклонная
                gl.Draw(BeginMode.Polygon, () =>
                {
                    gl.Vertex(Vector3.Zero);
                    gl.Vertex(Vector3.UnitZ * -WingsWidth);
                    gl.Vertex(FrontWingsLength, -FrontWingsHeight, -WingsWidth);
                    gl.Vertex(FrontWingsLength, -FrontWingsHeight);
                });

                // задняя наклонная
                gl.Draw(BeginMode.Polygon, () =>
                {
                    gl.Vertex(Vector3.Zero);
                    gl.Vertex(-BumperLength, -BumperMiddleHeight, 0);
                    gl.Vertex(-BumperLength, -BumperMiddleHeight, -WingsWidth);
                    gl.Vertex(Vector3.UnitZ * -WingsWidth);
                }, Vector3.UnitX * -(BaseLength + BumperLength), Vector3.Zero);

                // передние стенки крыльев
                gl.Draw(BeginMode.Polygon, () =>
                {
                    gl.Vertex(Vector3.UnitZ * zDiff);
                    gl.Vertex(FrontWingsLength, -FrontWingsHeight, zDiff);
                    gl.Vertex(0, -FrontWingsHeight, zDiff);
                    gl.Vertex(-BumperLength, 0, zDiff);
                });
                zDiff = -WingsWidth;
            }, 2, Vector3.UnitZ * (BaseWidth + WingsWidth), Vector3.Zero);
        }

        private static void DrawCabinet(OpenGL gl)
        {
            // середина (кабина)
            gl.DoTranslate(-BumperLength, 0, BaseWidth / 2f, true);

            // передняя и задняя наклонные
            gl.Repeat(() =>
            {
                gl.Draw(BeginMode.Polygon, () =>
                {
                    gl.Vertex(Vector3.UnitZ * (-CabinetBottomWidth / 2));
                    gl.Vertex(Vector3.UnitZ * (CabinetBottomWidth / 2));
                    gl.Vertex(-CabinetSlopeLength, CabinetHeight, CabinetTopWidth / 2);
                    gl.Vertex(-CabinetSlopeLength, CabinetHeight, -CabinetTopWidth / 2);
                });
            }, 2, Vector3.UnitX * -BaseLength, Vector3.UnitY * 180);

            // боковые наклонные
            var zDiff = 1;
            gl.Repeat(() =>
            {
                gl.Draw(BeginMode.Polygon, () =>
                {
                    gl.Vertex(Vector3.UnitZ * (CabinetBottomWidth / 2 * zDiff));
                    gl.Vertex(-BaseLength, 0, CabinetBottomWidth / 2 * zDiff);
                    gl.Vertex(-(BaseLength - CabinetSlopeLength), CabinetHeight, CabinetTopWidth / 2 * zDiff);
                    gl.Vertex(-CabinetSlopeLength, CabinetHeight, CabinetTopWidth / 2 * zDiff);
                });
                zDiff = -1;
            }, 2, Vector3.Zero, Vector3.Zero);

            // крышка
            gl.Draw(BeginMode.Polygon, () =>
            {
                gl.Vertex(-CabinetSlopeLength, CabinetHeight, CabinetTopWidth / 2);
                gl.Vertex(-(BaseLength - CabinetSlopeLength), CabinetHeight, CabinetTopWidth / 2);
                gl.Vertex(-(BaseLength - CabinetSlopeLength), CabinetHeight, -CabinetTopWidth / 2);
                gl.Vertex(-CabinetSlopeLength, CabinetHeight, -CabinetTopWidth / 2);
            });
        }

        private static void DrawTurret(OpenGL gl)
        {
            gl.DoTranslate(-CabinetSlopeLength - 6, CabinetHeight, 0, true);

            // основа башни
            gl.Rotate(Vector3.UnitX * -90, true);
            gl.DrawCylinder(TurretBaseRadius, TurretBaseHeight, partsCount: 10, withEdges: true);
            gl.UndoRotation();

            // башня
            gl.DoTranslate(Vector3.UnitY * TurretBaseHeight, true);

            gl.Repeat(() =>
            {
                // боковая средняя пластина
                gl.Draw(BeginMode.Polygon, () =>
                {
                    gl.Vertex(TurretMiddleWidth, 0, TurretBottomMiddleWidth / 2);
                    gl.Vertex(-TurretMiddleWidth, 0, TurretBottomMiddleWidth / 2);
                    gl.Vertex(-TurretMiddleWidth, TurretHeight, TurretTopMiddleWidth / 2);
                    gl.Vertex(TurretMiddleWidth, TurretHeight, TurretTopMiddleWidth / 2);
                });

                // боковая передняя пластина
                gl.Draw(BeginMode.Polygon, () =>
                {
                    gl.Vertex(TurretMiddleWidth, 0, TurretBottomMiddleWidth / 2);
                    gl.Vertex(TurretMiddleWidth, TurretHeight, TurretTopMiddleWidth / 2);
                    gl.Vertex(TurretLength, TurretHeight, TurretTopFrontWidth / 2);
                    gl.Vertex(TurretLength, 0, TurretBottomFrontWidth / 2);
                });

                // боковая задняя пластина
                gl.Draw(BeginMode.Polygon, () =>
                {
                    gl.Vertex(-TurretMiddleWidth, 0, TurretBottomMiddleWidth / 2);
                    gl.Vertex(-TurretLength, 0, TurretBottomFrontWidth / 2);
                    gl.Vertex(-TurretLength, TurretHeight, TurretTopFrontWidth / 2);
                    gl.Vertex(-TurretMiddleWidth, TurretHeight, TurretTopMiddleWidth / 2);
                });
            }, 2, Vector3.Zero, Vector3.UnitY * 180);

            // передняя пластина

            gl.Repeat(() =>
            {
                gl.Draw(BeginMode.Polygon, () =>
                {
                    gl.Vertex(TurretLength, 0, TurretBottomFrontWidth / 2);
                    gl.Vertex(TurretLength, TurretHeight, TurretTopFrontWidth / 2);
                    gl.Vertex(TurretLength, TurretHeight, -TurretTopFrontWidth / 2);
                    gl.Vertex(TurretLength, 0, -TurretBottomFrontWidth / 2);
                });
            }, 2, Vector3.Zero, Vector3.UnitY * 180);

            // крышка
            gl.Draw(BeginMode.Polygon, () =>
            {
                gl.Vertex(TurretLength, TurretHeight, TurretTopFrontWidth / 2);
                gl.Vertex(TurretMiddleWidth, TurretHeight, TurretTopMiddleWidth / 2);
                gl.Vertex(-TurretMiddleWidth, TurretHeight, TurretTopMiddleWidth / 2);
                gl.Vertex(-TurretLength, TurretHeight, TurretTopFrontWidth / 2);

                gl.Vertex(-TurretLength, TurretHeight, -TurretTopFrontWidth / 2);
                gl.Vertex(-TurretMiddleWidth, TurretHeight, -TurretTopMiddleWidth / 2);
                gl.Vertex(TurretMiddleWidth, TurretHeight, -TurretTopMiddleWidth / 2);
                gl.Vertex(TurretLength, TurretHeight, -TurretTopFrontWidth / 2);
            }, true);

            //основа ствола
            gl.DoTranslate(Vector3.UnitX * TurretLength, true);

            // нижняя и верхняя пластина
            gl.Repeat(() =>
            {
                gl.Draw(BeginMode.Polygon, () =>
                {
                    gl.Vertex(BarrelBaseLength, 0, BarrelBaseWidth / 2);
                    gl.Vertex(BarrelBaseLength, 0, -BarrelBaseWidth / 2);
                    gl.Vertex(0, 0, -BarrelBaseWidth / 2);
                    gl.Vertex(0, 0, BarrelBaseWidth / 2);
                });
            }, 2, Vector3.UnitY * BarrelBaseHeight, Vector3.Zero);

            //боковые части
            gl.Repeat(() =>
            {
                gl.Draw(BeginMode.Quads, () =>
                {
                    gl.Vertex(Vector3.UnitZ * BarrelBaseWidth / 2);
                    gl.Vertex(0, BarrelBaseHeight, BarrelBaseWidth / 2);
                    gl.Vertex(BarrelBaseLength, BarrelBaseHeight, BarrelBaseWidth / 2);
                    gl.Vertex(BarrelBaseLength, 0, BarrelBaseWidth / 2);
                });
            }, 2, Vector3.UnitX * BarrelBaseLength, Vector3.UnitY * 180);

            // передняя крышка
            gl.Draw(BeginMode.Quads, () =>
            {
                gl.Vertex(BarrelBaseLength, 0, BarrelBaseWidth / 2);
                gl.Vertex(BarrelBaseLength, BarrelBaseHeight, BarrelBaseWidth / 2);
                gl.Vertex(BarrelBaseLength, BarrelBaseHeight, -BarrelBaseWidth / 2);
                gl.Vertex(BarrelBaseLength, 0, -BarrelBaseWidth / 2);
            });

            // дуло
            gl.DoTranslate(BarrelBaseLength, BarrelBaseHeight / 2, 0, true);

            gl.Rotate(Vector3.UnitZ * -90, true);
            gl.Rotate(Vector3.UnitX * -90, true);

            gl.SetColor(Color.DarkSlateGray);
            gl.DrawCylinder(BarrelRadius, BarrelLength, partsCount: Roundness);

            gl.UndoRotation(2);
        }

        private static void DrawRunningGear(OpenGL gl)
        {
            gl.ResetTranslations();

            const float zDiff = BaseWidth + WheelPartWidth + WheelConnectorWidth;
            gl.DoTranslate(0, -(BaseHeight / 2 + 1.5f), zDiff, true);

            gl.Repeat(() =>
            {
                //колеса
                DrawWheels(gl);
                gl.UndoTranslation();

                // гусеницы
                DrawTracks(gl);
                gl.UndoTranslation(2);
            }, 2, Vector3.UnitZ * - (zDiff + WheelPartWidth), Vector3.Zero);
        }

        private static void DrawTracks(OpenGL gl)
        {
            // вокруг заднего колеса
            gl.DoTranslate(
                -(BigWheelOuterRadius + SmallWheelOuterRadius + DistanceBetweenWheels),
                SmallWheelHeight,
                -(WheelPartWidth + WheelConnectorWidth),
                true);

            // вокруг
            const int trackUnitAroundWheelCount = 8;
            gl.Repeat(() =>
            {
                gl.DoTranslate(TrackUnitLength / 2, SmallWheelOuterRadius, 0, true);
                DrawTrackUnit(gl);
                gl.UndoTranslation();
            }, trackUnitAroundWheelCount, Vector3.Zero, Vector3.UnitZ * 23);

            // нижние соединительные плашки
            gl.Rotate(Vector3.UnitZ * 23 * (trackUnitAroundWheelCount - 1), true);
            gl.DoTranslate(
                -(TrackUnitLength / 2 + TrackUnitConnectorLength + TrackUnitsGap),
                SmallWheelOuterRadius,
                0,
                true);
            var trackUnitDiff = Vector3.UnitX * (TrackUnitConnectorLength + TrackUnitLength + TrackUnitsGap);

            gl.Repeat(() => DrawTrackUnit(gl), 4, -trackUnitDiff, Vector3.Zero);

            gl.UndoTranslation();
            gl.UndoRotation();

            // соединительная верхняя плашка
            const float xDiff = TrackUnitLength / 2 + TrackUnitsGap;
            gl.DoTranslate(xDiff, SmallWheelOuterRadius, 0, true);
            gl.Rotate(Vector3.UnitZ * -12, true);
            gl.DoTranslate(Vector3.UnitX * (TrackUnitConnectorLength + TrackUnitLength), true);
            DrawTrackUnit(gl);
            gl.UndoTranslation();
            gl.UndoRotation();

            gl.UndoTranslation(2);

            // вокруг переднего
            const float wheelDiff = BigWheelOuterRadius * 2 + DistanceBetweenWheels;
            gl.DoTranslate(
                wheelDiff * BigWheelsCount,
                SmallWheelHeight,
                -(WheelPartWidth + WheelConnectorWidth),
                true);
            gl.Rotate(Vector3.UnitZ * -6, true);

            // вокруг
            gl.Repeat(() =>
            {
                gl.DoTranslate(TrackUnitLength / 2, SmallWheelOuterRadius, 0, true);
                DrawTrackUnit(gl);
                gl.UndoTranslation();
            }, trackUnitAroundWheelCount, Vector3.Zero, Vector3.UnitZ * -23);

            // нижние соединительные плашки
            gl.Rotate(Vector3.UnitZ * -23 * (trackUnitAroundWheelCount - 1), true);
            gl.DoTranslate(
                TrackUnitLength * 1.5f + TrackUnitConnectorLength + TrackUnitsGap,
                SmallWheelOuterRadius,
                0,
                true);

            gl.Repeat(() => DrawTrackUnit(gl), 6, trackUnitDiff, Vector3.Zero);

            gl.UndoTranslation();
            gl.UndoRotation();

            // соединительная верхняя плашка
            gl.DoTranslate(
                -(TrackUnitLength / 2 + TrackUnitsGap),
                SmallWheelOuterRadius,
                0,
                true);
            gl.Rotate(Vector3.UnitZ * 20, true);
            gl.DoTranslate(Vector3.UnitX * -TrackUnitConnectorLength, true);
            DrawTrackUnit(gl);

            gl.UndoTranslation();
            gl.UndoRotation();
            gl.UndoTranslation();

            gl.UndoRotation();
            gl.UndoTranslation();

            // верхние плашки
            gl.DoTranslate(
                -(BigWheelOuterRadius + TrackUnitLength + TrackUnitsGap),
                BigWheelOuterRadius,
                -(WheelPartWidth + WheelConnectorWidth),
                true);

            const int upperTrackUnitsCount = 36;

            gl.Repeat(() => DrawTrackUnit(gl), upperTrackUnitsCount, trackUnitDiff, Vector3.Zero);

            // нижние плашки
            gl.DoTranslate(TrackUnitLength * 2, -BigWheelOuterRadius * 2, 0, true);
            gl.Rotate(Vector3.UnitZ * 180, true);

            const int lowerTrackUnitsCount = 30;

            gl.Repeat(() => DrawTrackUnit(gl), lowerTrackUnitsCount, -trackUnitDiff, Vector3.Zero);

            gl.UndoRotation();
        }

        private static void DrawWheels(OpenGL gl)
        {
            gl.SetColor(Color.DimGray);
            var wheelDiff = Vector3.UnitX * (BigWheelOuterRadius * 2 + DistanceBetweenWheels);
            gl.Repeat(() => DrawBigWheel(gl), BigWheelsCount, wheelDiff, Vector3.Zero);

            gl.SetColor(81, 80, 63);
            gl.DoTranslate(
                -(BigWheelOuterRadius + SmallWheelOuterRadius + DistanceBetweenWheels),
                SmallWheelHeight,
                0,
                true);
            DrawSmallWheel(gl);
            gl.UndoTranslation();
            gl.DoTranslate(wheelDiff * BigWheelsCount + Vector3.UnitY * SmallWheelHeight, true);
            DrawSmallWheel(gl);
        }

        private static void DrawBigWheel(OpenGL gl)
        {
            // половинки колеса
            gl.Repeat(() =>
            {
                // внешняя часть
                gl.DrawCylinder(BigWheelOuterRadius, WheelPartWidth, partsCount: Roundness);
                gl.DrawCylinder(BigWheelInnerRadius, WheelPartWidth, partsCount: Roundness);

                // задняя стенка
                gl.Color(.24f, .25f, .22f);
                gl.DrawCircle(BigWheelOuterRadius, Roundness);

                // шайбы в середине
                gl.DrawCylinder(BigWheelInnerPartRadius1, BigWheelInnerPartWidth, partsCount: Roundness);
                gl.DoTranslate(Vector3.UnitZ * BigWheelInnerPartWidth, true);
                gl.DrawCircle(BigWheelInnerPartRadius1, Roundness);
                gl.DrawCylinder(BigWheelInnerPartRadius2, BigWheelInnerPartWidth, partsCount: Roundness);
                gl.DoTranslate(Vector3.UnitZ * BigWheelInnerPartWidth, true);
                gl.DrawCircle(BigWheelInnerPartRadius2, Roundness);

                // закрывашка внешненей части (между цилиндрами)
                gl.UndoTranslation(2);
                gl.DoTranslate(Vector3.UnitZ * WheelPartWidth, true);
                gl.DrawDisk(BigWheelInnerRadius, BigWheelOuterRadius, Roundness, true);

                gl.UndoTranslation();
            }, 2, Vector3.UnitZ * -WheelConnectorWidth, Vector3.UnitY * 180);

            // соединитель
            gl.DrawCylinder(BigWheelConnectorRadius, -WheelConnectorWidth, partsCount: Roundness);
        }

        public static void DrawSmallWheel(OpenGL gl)
        {
            gl.Repeat(() =>
            {
                // внешняя часть
                gl.DrawCylinder(SmallWheelOuterRadius, WheelPartWidth, partsCount: Roundness);
                gl.DrawCylinder(SmallWheelInnerRadius, WheelPartWidth, partsCount: Roundness);
                // задняя стенка
                gl.DrawCircle(SmallWheelOuterRadius, Roundness);
                // крышка внешней части
                gl.DoTranslate(Vector3.UnitZ * WheelPartWidth, true);
                gl.DrawDisk(SmallWheelInnerRadius, SmallWheelOuterRadius, Roundness);

                gl.UndoTranslation();
                // кольцо в середине
                gl.DrawCylinder(SmallWheelRingRadius1, SmallWheelRingWidth, partsCount: Roundness);
                gl.DrawCylinder(SmallWheelRingRadius2, SmallWheelRingWidth, partsCount: Roundness);
                gl.DoTranslate(Vector3.UnitZ * SmallWheelRingWidth, true);
                gl.DrawDisk(SmallWheelRingRadius1, SmallWheelRingRadius2, Roundness);
                gl.UndoTranslation();
            }, 2, Vector3.UnitZ * -WheelConnectorWidth, Vector3.UnitY * 180);

            // соединитель
            gl.DrawCylinder(BigWheelConnectorRadius, -WheelConnectorWidth, partsCount: Roundness);
        }

        public static void DrawTrackUnit(OpenGL gl)
        {
            gl.SetColor(Color.Black);
            gl.SetColor(42, 42, 42, true);
            gl.Rotate(Vector3.UnitY * -90, true);

            // основная плашка
            gl.DrawParallelepiped(new(TrackUnitWidth, TrackUnitThickness, TrackUnitLength), true);

            // передние соединительные звенья
            gl.DoTranslate(Vector3.UnitX * TrackUnitConnectorWidth, true);
            gl.Repeat(() =>
            {
                var edges = new Vector3(TrackUnitConnectorWidth, TrackUnitThickness, -TrackUnitConnectorLength);
                gl.DrawParallelepiped(edges, true);
            }, 4, Vector3.UnitX * TrackUnitConnectorWidth * 2, Vector3.Zero);

            gl.UndoTranslation();
            // задние соединители
            gl.DoTranslate(Vector3.UnitZ * TrackUnitLength, true);
            gl.Repeat(() =>
            {
                var edges = new Vector3(TrackUnitConnectorWidth, TrackUnitThickness, TrackUnitConnectorLength);
                gl.DrawParallelepiped(edges, true);
            }, 5, Vector3.UnitX * TrackUnitConnectorWidth * 2, Vector3.Zero);

            gl.UndoTranslation();
            gl.UndoRotation();
            gl.SetColor(Color.Black, true);
        }
    }
}