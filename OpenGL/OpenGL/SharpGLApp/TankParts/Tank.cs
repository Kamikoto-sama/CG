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
        private const float BaseLength = 25;
        private const float BaseHeight = 3.5f;
        private const float BaseWidth = 10;

        // передине и задние наклонные (бампер)
        private const float BumperLength = 3.5f;
        private const float BumperMiddleHeight = BaseHeight / 2f;

        // крылья
        private const float FrontWingsLength = 2;
        private const float FrontWingsHeight = 2;
        private const float WingsWidth = 3;

        // кабина (середина)
        private const float CabinetBottomWidth = BaseWidth + 4;
        private const float CabinetTopWidth = BaseWidth;
        private const float CabinetHeight = 3;
        private const float CabinetSlopeLength = 5;

        // основа башни (круглая часть под башней)
        private const float TurretBaseRadius = CabinetTopWidth / 2 - 1;

        private const float TurretBaseHeight = 0.5f;

        // башня
        private const float TurretMiddleWidth = 0.5f;
        private const float TurretBottomMiddleWidth = CabinetTopWidth - 2;
        private const float TurretTopMiddleWidth = TurretBottomMiddleWidth - 2.5f;
        private const float TurretTopFrontWidth = TurretTopMiddleWidth - 2;
        private const float TurretBottomFrontWidth = TurretTopFrontWidth + 2f;
        private const float TurretHeight = 2.5f;
        private const float TurretLength = 5;

        //основание ствола
        private const float BarrelBaseLength = 1.5f;
        private const float BarrelBaseWidth = 1.5f;

        private const float BarrelBaseHeight = 2f;

        // дуло
        private const float BarrelLength = 9;
        private const float BarrelRadius = 0.25f;

        // большое колесо
        private const float WheelPartWidth = .5f;
        private const float BigWheelOuterRadius = 1.5f;
        private const float BigWheelInnerRadius = BigWheelOuterRadius * .85f;
        private const float BigWheelInnerPartRadius1 = BigWheelOuterRadius * .3f;
        private const float BigWheelInnerPartRadius2 = BigWheelInnerPartRadius1 * .8f;
        private const float BigWheelInnerPartWidth = .1f;
        private const float BigWheelConnectorWidth = .5f;
        private const float BigWheelConnectorRadius = BigWheelInnerPartRadius1;

        public static void Draw(OpenGL gl)
        {
            // Основа
            DrawBase(gl);

            // Кабина (середина)
            DrawCabinet(gl);

            // башня
            DrawTurret(gl);

            // гусеницы
            DrawTracks(gl);
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
            gl.DrawCylinder(TurretBaseRadius, TurretBaseHeight, partsCount: 10);
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
            gl.DrawCylinder(BarrelRadius, BarrelLength, partsCount: Roundness, edgeless: true);

            gl.UndoRotation(2);
        }

        private static void DrawTracks(OpenGL gl)
        {
            gl.ResetTranslations();
            gl.DoTranslate(0, -(BaseHeight / 2 + 1.5f), BaseWidth + 1.1f, true);

            //колеса
            gl.SetColor(Color.DimGray);
            DrawBigWheel(gl);

            gl.DoTranslate(-BigWheelOuterRadius * 2, BaseHeight / 2, 0);
        }

        private static void DrawBigWheel(OpenGL gl)
        {
            // половинки колеса
            gl.Repeat(() =>
            {
                // внешняя часть
                gl.DrawCylinder(BigWheelOuterRadius, WheelPartWidth, partsCount: Roundness, edgeless: true);
                gl.DrawCylinder(BigWheelInnerRadius, WheelPartWidth, partsCount: Roundness, edgeless: true);

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
            }, 2, Vector3.UnitZ * -BigWheelConnectorWidth, Vector3.UnitY * 180);

            // соединитель
            gl.DrawCylinder(BigWheelConnectorRadius, -BigWheelConnectorWidth, partsCount: Roundness, edgeless: true);
        }

        public static void DrawSmallWheel(OpenGL gl)
        {
            const float smallWheelOuterRadius = BigWheelOuterRadius * .9f;
            const float smallWheelInnerRadius = smallWheelOuterRadius * .9f;

            gl.DrawCylinder(smallWheelOuterRadius, WheelPartWidth, partsCount: Roundness, edgeless: true);
            gl.DrawCylinder(smallWheelInnerRadius, WheelPartWidth, partsCount: Roundness, edgeless: true);
        }
    }
}