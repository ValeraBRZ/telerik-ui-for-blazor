using Microsoft.Extensions.AI;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Telerik.SvgIcons;

namespace TelerikBlazorDemos.Services.Data
{
    public class MedicalRecordService
    {
        public List<MedicalRecordDto> GetData()
        {
            var data = new List<MedicalRecordDto>()
            {
    new MedicalRecordDto()
                {
                    Id = 1,
                    PatientName = "Eduardo Saavedra",
                    Diagnosis = "Asthma",
                    Status = "Under Treatment",
                    MedicationCount = 3,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 3, 19, 3, 49, 47, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 2,
                    PatientName = "Karin Josephs",
                    Diagnosis = "Artritis",
                    Status = "Awaiting Diagnosis",
                    MedicationCount = 3,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 12, 14, 6, 19, 37, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 3,
                    PatientName = "Christina Berglund",
                    Diagnosis = "Hepatitis",
                    Status = "Discharged",
                    MedicationCount = 6,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 7, 19, 1, 1, 45, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 4,
                    PatientName = "Rita Müller",
                    Diagnosis = "Artery diseas",
                    Status = "Transferred",
                    MedicationCount = 3,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 8, 18, 10, 45, 33, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 5,
                    PatientName = "Guillermo Fernández",
                    Diagnosis = "Hyperthyroidism",
                    Status = "Monitoring",
                    MedicationCount = 7,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 2, 13, 3, 30, 27, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 6,
                    PatientName = "Patricia McKenna",
                    Diagnosis = "Skin disease",
                    Status = "Stable",
                    MedicationCount = 5,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 4, 27, 4, 33, 30, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 7,
                    PatientName = "Georg Pipps",
                    Diagnosis = "Kidney disease",
                    Status = "Active",
                    MedicationCount = 5,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 12, 9, 9, 9, 39, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 8,
                    PatientName = "Janine Labrune",
                    Diagnosis = "Asthma",
                    Status = "Under Treatment",
                    MedicationCount = 8,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 4, 18, 4, 10, 13, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 9,
                    PatientName = "Henriette Pfalzheim",
                    Diagnosis = "Artritis",
                    Status = "Awaiting Diagnosis",
                    MedicationCount = 3,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 7, 18, 9, 29, 23, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 10,
                    PatientName = "Fran Wilson",
                    Diagnosis = "Hepatitis",
                    Status = "Discharged",
                    MedicationCount = 6,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 10, 8, 2, 46, 2, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 11,
                    PatientName = "Sergio Gutiérrez",
                    Diagnosis = "Artery diseas",
                    Status = "Transferred",
                    MedicationCount = 3,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 12, 14, 3, 5, 6, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 12,
                    PatientName = "Christina Berglund",
                    Diagnosis = "Hyperthyroidism",
                    Status = "Monitoring",
                    MedicationCount = 8,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 1, 8, 3, 8, 47, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 13,
                    PatientName = "Felipe Izquierdo",
                    Diagnosis = "Skin disease",
                    Status = "Stable",
                    MedicationCount = 5,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 8, 14, 5, 29, 28, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 14,
                    PatientName = "Frédérique Citeaux",
                    Diagnosis = "Kidney disease",
                    Status = "Active",
                    MedicationCount = 3,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 5, 26, 4, 52, 50, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 15,
                    PatientName = "Frédérique Citeaux",
                    Diagnosis = "Asthma",
                    Status = "Under Treatment",
                    MedicationCount = 1,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 5, 14, 8, 31, 47, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 16,
                    PatientName = "Sergio Gutiérrez",
                    Diagnosis = "Artritis",
                    Status = "Awaiting Diagnosis",
                    MedicationCount = 5,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 10, 21, 7, 37, 57, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 17,
                    PatientName = "Liu Wong",
                    Diagnosis = "Hepatitis",
                    Status = "Discharged",
                    MedicationCount = 7,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 1, 12, 7, 12, 13, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 18,
                    PatientName = "Eduardo Saavedra",
                    Diagnosis = "Artery diseas",
                    Status = "Transferred",
                    MedicationCount = 6,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 4, 26, 2, 14, 15, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 19,
                    PatientName = "Noah Smith",
                    Diagnosis = "Hyperthyroidism",
                    Status = "Monitoring",
                    MedicationCount = 2,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 12, 12, 8, 26, 49, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 20,
                    PatientName = "Elizabeth Lincoln",
                    Diagnosis = "Skin disease",
                    Status = "Stable",
                    MedicationCount = 4,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 10, 20, 2, 34, 41, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 21,
                    PatientName = "Sergio Gutiérrez",
                    Diagnosis = "Kidney disease",
                    Status = "Active",
                    MedicationCount = 2,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 1, 25, 8, 52, 12, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 22,
                    PatientName = "Jonas Bergulfsen",
                    Diagnosis = "Asthma",
                    Status = "Under Treatment",
                    MedicationCount = 3,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 4, 25, 5, 37, 51, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 23,
                    PatientName = "Matti Karttunen",
                    Diagnosis = "Artritis",
                    Status = "Awaiting Diagnosis",
                    MedicationCount = 5,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 10, 18, 5, 36, 46, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 24,
                    PatientName = "Janete Limeira",
                    Diagnosis = "Hepatitis",
                    Status = "Discharged",
                    MedicationCount = 8,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 1, 8, 10, 55, 16, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 25,
                    PatientName = "Matti Karttunen",
                    Diagnosis = "Artery diseas",
                    Status = "Transferred",
                    MedicationCount = 5,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 3, 26, 3, 32, 20, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 26,
                    PatientName = "Pascale Cartrain",
                    Diagnosis = "Hyperthyroidism",
                    Status = "Monitoring",
                    MedicationCount = 8,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 10, 6, 8, 4, 15, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 27,
                    PatientName = "Antonio Moreno",
                    Diagnosis = "Skin disease",
                    Status = "Stable",
                    MedicationCount = 3,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 1, 23, 2, 27, 5, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 28,
                    PatientName = "Howard Snyder",
                    Diagnosis = "Kidney disease",
                    Status = "Active",
                    MedicationCount = 2,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 5, 16, 6, 6, 9, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 29,
                    PatientName = "Sven Ottlieb",
                    Diagnosis = "Asthma",
                    Status = "Under Treatment",
                    MedicationCount = 8,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 5, 21, 8, 21, 32, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 30,
                    PatientName = "Martín Sommer",
                    Diagnosis = "Artritis",
                    Status = "Awaiting Diagnosis",
                    MedicationCount = 1,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 9, 3, 1, 52, 52, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 31,
                    PatientName = "Paolo Accorti",
                    Diagnosis = "Hepatitis",
                    Status = "Discharged",
                    MedicationCount = 8,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 9, 11, 1, 53, 40, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 32,
                    PatientName = "Hari Kumar",
                    Diagnosis = "Artery diseas",
                    Status = "Transferred",
                    MedicationCount = 5,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 3, 12, 1, 37, 48, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 33,
                    PatientName = "Dominique Perrier",
                    Diagnosis = "Hyperthyroidism",
                    Status = "Monitoring",
                    MedicationCount = 6,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 4, 2, 7, 27, 49, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 34,
                    PatientName = "Alejandra Camino",
                    Diagnosis = "Skin disease",
                    Status = "Stable",
                    MedicationCount = 3,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 2, 6, 4, 19, 52, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 35,
                    PatientName = "Laurence Lebihan",
                    Diagnosis = "Kidney disease",
                    Status = "Active",
                    MedicationCount = 7,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 4, 26, 2, 42, 52, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 36,
                    PatientName = "Art Braunschweiger",
                    Diagnosis = "Asthma",
                    Status = "Under Treatment",
                    MedicationCount = 7,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 6, 21, 4, 40, 46, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 37,
                    PatientName = "Jean Fresnière",
                    Diagnosis = "Artritis",
                    Status = "Awaiting Diagnosis",
                    MedicationCount = 8,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 6, 8, 10, 37, 40, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 38,
                    PatientName = "Lúcia Carvalho",
                    Diagnosis = "Hepatitis",
                    Status = "Discharged",
                    MedicationCount = 1,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 1, 1, 8, 51, 29, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 39,
                    PatientName = "James Miller",
                    Diagnosis = "Artery diseas",
                    Status = "Transferred",
                    MedicationCount = 6,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 6, 23, 8, 12, 38, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 40,
                    PatientName = "Noah Smith",
                    Diagnosis = "Hyperthyroidism",
                    Status = "Monitoring",
                    MedicationCount = 2,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 7, 6, 10, 6, 53, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 41,
                    PatientName = "Paolo Accorti",
                    Diagnosis = "Skin disease",
                    Status = "Stable",
                    MedicationCount = 1,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 7, 10, 6, 46, 54, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 42,
                    PatientName = "Annette Roulet",
                    Diagnosis = "Kidney disease",
                    Status = "Active",
                    MedicationCount = 6,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 4, 20, 2, 42, 8, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 43,
                    PatientName = "Felipe Izquierdo",
                    Diagnosis = "Asthma",
                    Status = "Under Treatment",
                    MedicationCount = 2,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 5, 10, 7, 6, 13, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 44,
                    PatientName = "Roland Mendel",
                    Diagnosis = "Artritis",
                    Status = "Awaiting Diagnosis",
                    MedicationCount = 2,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 5, 1, 9, 8, 19, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 45,
                    PatientName = "Rita Müller",
                    Diagnosis = "Hepatitis",
                    Status = "Discharged",
                    MedicationCount = 6,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 12, 12, 8, 50, 34, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 46,
                    PatientName = "Yoshi Tannamuri",
                    Diagnosis = "Artery diseas",
                    Status = "Transferred",
                    MedicationCount = 8,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 4, 6, 7, 55, 2, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 47,
                    PatientName = "John Steel",
                    Diagnosis = "Hyperthyroidism",
                    Status = "Monitoring",
                    MedicationCount = 4,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 10, 1, 6, 35, 15, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 48,
                    PatientName = "Helvetius Nagy",
                    Diagnosis = "Skin disease",
                    Status = "Stable",
                    MedicationCount = 7,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 6, 16, 9, 52, 44, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 49,
                    PatientName = "Diego Roel",
                    Diagnosis = "Kidney disease",
                    Status = "Active",
                    MedicationCount = 5,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 2, 8, 3, 37, 18, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 50,
                    PatientName = "Laurence Lebihan",
                    Diagnosis = "Asthma",
                    Status = "Under Treatment",
                    MedicationCount = 5,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 9, 15, 7, 48, 56, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 51,
                    PatientName = "Patricia McKenna",
                    Diagnosis = "Artritis",
                    Status = "Awaiting Diagnosis",
                    MedicationCount = 8,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 6, 14, 3, 5, 1, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 52,
                    PatientName = "Yvonne Moncada",
                    Diagnosis = "Hepatitis",
                    Status = "Discharged",
                    MedicationCount = 7,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 6, 26, 3, 35, 6, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 53,
                    PatientName = "Georg Pipps",
                    Diagnosis = "Artery diseas",
                    Status = "Transferred",
                    MedicationCount = 3,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 6, 19, 6, 37, 51, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 54,
                    PatientName = "Miguel Angel Paolino",
                    Diagnosis = "Hyperthyroidism",
                    Status = "Monitoring",
                    MedicationCount = 7,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 3, 18, 5, 31, 51, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 55,
                    PatientName = "Michael Holz",
                    Diagnosis = "Skin disease",
                    Status = "Stable",
                    MedicationCount = 6,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 1, 7, 5, 36, 5, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 56,
                    PatientName = "Yoshi Tannamuri",
                    Diagnosis = "Kidney disease",
                    Status = "Active",
                    MedicationCount = 8,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 6, 17, 3, 5, 55, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 57,
                    PatientName = "Catherine Dewey",
                    Diagnosis = "Asthma",
                    Status = "Under Treatment",
                    MedicationCount = 7,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 9, 24, 6, 26, 23, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 58,
                    PatientName = "Pascale Cartrain",
                    Diagnosis = "Artritis",
                    Status = "Awaiting Diagnosis",
                    MedicationCount = 4,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 1, 17, 10, 2, 1, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 59,
                    PatientName = "Carlos González",
                    Diagnosis = "Hepatitis",
                    Status = "Discharged",
                    MedicationCount = 2,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 4, 25, 1, 14, 3, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 60,
                    PatientName = "Liu Wong",
                    Diagnosis = "Artery diseas",
                    Status = "Transferred",
                    MedicationCount = 6,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 11, 6, 8, 34, 24, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 61,
                    PatientName = "Christina Berglund",
                    Diagnosis = "Hyperthyroidism",
                    Status = "Monitoring",
                    MedicationCount = 4,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 11, 10, 4, 44, 4, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 62,
                    PatientName = "Helen Bennett",
                    Diagnosis = "Skin disease",
                    Status = "Stable",
                    MedicationCount = 5,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 5, 9, 9, 49, 5, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 63,
                    PatientName = "Hanna Moos",
                    Diagnosis = "Kidney disease",
                    Status = "Active",
                    MedicationCount = 5,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 4, 6, 6, 42, 24, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 64,
                    PatientName = "Palle Ibsen",
                    Diagnosis = "Asthma",
                    Status = "Under Treatment",
                    MedicationCount = 1,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 8, 6, 9, 13, 14, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 65,
                    PatientName = "Sven Ottlieb",
                    Diagnosis = "Artritis",
                    Status = "Awaiting Diagnosis",
                    MedicationCount = 1,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 8, 25, 3, 44, 7, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 66,
                    PatientName = "Yoshi Latimer",
                    Diagnosis = "Hepatitis",
                    Status = "Discharged",
                    MedicationCount = 6,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 3, 7, 2, 50, 10, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 67,
                    PatientName = "Guillermo Fernández",
                    Diagnosis = "Artery diseas",
                    Status = "Transferred",
                    MedicationCount = 1,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 7, 1, 3, 36, 40, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 68,
                    PatientName = "Felipe Izquierdo",
                    Diagnosis = "Hyperthyroidism",
                    Status = "Monitoring",
                    MedicationCount = 4,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 3, 27, 4, 36, 50, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 69,
                    PatientName = "Pedro Afonso",
                    Diagnosis = "Skin disease",
                    Status = "Stable",
                    MedicationCount = 1,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 10, 9, 3, 28, 44, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 70,
                    PatientName = "Yvonne Moncada",
                    Diagnosis = "Kidney disease",
                    Status = "Active",
                    MedicationCount = 1,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 5, 25, 5, 49, 6, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 71,
                    PatientName = "Helvetius Nagy",
                    Diagnosis = "Asthma",
                    Status = "Under Treatment",
                    MedicationCount = 6,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 1, 27, 6, 38, 41, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 72,
                    PatientName = "Maria Larsson",
                    Diagnosis = "Artritis",
                    Status = "Awaiting Diagnosis",
                    MedicationCount = 4,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 5, 12, 8, 32, 32, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 73,
                    PatientName = "Alejandra Camino",
                    Diagnosis = "Hepatitis",
                    Status = "Discharged",
                    MedicationCount = 8,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 9, 4, 6, 28, 27, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 74,
                    PatientName = "Yoshi Latimer",
                    Diagnosis = "Artery diseas",
                    Status = "Transferred",
                    MedicationCount = 6,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 6, 5, 10, 47, 38, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 75,
                    PatientName = "Matti Karttunen",
                    Diagnosis = "Hyperthyroidism",
                    Status = "Monitoring",
                    MedicationCount = 7,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 3, 27, 10, 13, 49, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 76,
                    PatientName = "Lúcia Carvalho",
                    Diagnosis = "Skin disease",
                    Status = "Stable",
                    MedicationCount = 7,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 4, 17, 4, 20, 46, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 77,
                    PatientName = "Fran Wilson",
                    Diagnosis = "Kidney disease",
                    Status = "Active",
                    MedicationCount = 2,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 12, 6, 8, 33, 23, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 78,
                    PatientName = "José Pedro Freyre",
                    Diagnosis = "Asthma",
                    Status = "Under Treatment",
                    MedicationCount = 8,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 8, 23, 7, 21, 24, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 79,
                    PatientName = "Lúcia Carvalho",
                    Diagnosis = "Artritis",
                    Status = "Awaiting Diagnosis",
                    MedicationCount = 5,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 2, 21, 6, 58, 32, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 80,
                    PatientName = "Carlos Hernández",
                    Diagnosis = "Hepatitis",
                    Status = "Discharged",
                    MedicationCount = 4,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 11, 5, 1, 47, 43, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 81,
                    PatientName = "Jonas Bergulfsen",
                    Diagnosis = "Artery diseas",
                    Status = "Transferred",
                    MedicationCount = 5,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 7, 23, 8, 28, 55, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 82,
                    PatientName = "Noah Smith",
                    Diagnosis = "Hyperthyroidism",
                    Status = "Monitoring",
                    MedicationCount = 1,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 4, 7, 1, 11, 15, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 83,
                    PatientName = "Maurizio Moroni",
                    Diagnosis = "Skin disease",
                    Status = "Stable",
                    MedicationCount = 8,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 4, 7, 10, 35, 40, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 84,
                    PatientName = "Alejandra Camino",
                    Diagnosis = "Kidney disease",
                    Status = "Active",
                    MedicationCount = 6,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 8, 22, 8, 16, 57, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 85,
                    PatientName = "Isabel de Castro",
                    Diagnosis = "Asthma",
                    Status = "Under Treatment",
                    MedicationCount = 7,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 2, 26, 5, 41, 17, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 86,
                    PatientName = "Bernardo Batista",
                    Diagnosis = "Artritis",
                    Status = "Awaiting Diagnosis",
                    MedicationCount = 2,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 2, 24, 3, 15, 24, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 87,
                    PatientName = "Rene Phillips",
                    Diagnosis = "Hepatitis",
                    Status = "Discharged",
                    MedicationCount = 7,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 10, 18, 4, 10, 57, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 88,
                    PatientName = "José Pedro Freyre",
                    Diagnosis = "Artery diseas",
                    Status = "Transferred",
                    MedicationCount = 1,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 12, 7, 10, 2, 11, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 89,
                    PatientName = "Paolo Accorti",
                    Diagnosis = "Hyperthyroidism",
                    Status = "Monitoring",
                    MedicationCount = 3,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 7, 20, 3, 33, 8, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 90,
                    PatientName = "Roland Mendel",
                    Diagnosis = "Skin disease",
                    Status = "Stable",
                    MedicationCount = 6,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 7, 24, 9, 25, 18, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 91,
                    PatientName = "Maria Anders",
                    Diagnosis = "Kidney disease",
                    Status = "Active",
                    MedicationCount = 1,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 3, 9, 10, 15, 44, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 92,
                    PatientName = "James Miller",
                    Diagnosis = "Asthma",
                    Status = "Under Treatment",
                    MedicationCount = 2,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 11, 10, 3, 20, 40, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 93,
                    PatientName = "Alejandra Camino",
                    Diagnosis = "Artritis",
                    Status = "Awaiting Diagnosis",
                    MedicationCount = 5,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 7, 27, 2, 31, 5, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 94,
                    PatientName = "Carine Schmitt",
                    Diagnosis = "Hepatitis",
                    Status = "Discharged",
                    MedicationCount = 3,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 1, 23, 8, 28, 5, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 95,
                    PatientName = "Horst Kloss",
                    Diagnosis = "Artery diseas",
                    Status = "Transferred",
                    MedicationCount = 3,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 8, 24, 2, 9, 17, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 96,
                    PatientName = "Palle Ibsen",
                    Diagnosis = "Hyperthyroidism",
                    Status = "Monitoring",
                    MedicationCount = 4,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 7, 10, 2, 22, 23, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 97,
                    PatientName = "Mario Pontes",
                    Diagnosis = "Skin disease",
                    Status = "Stable",
                    MedicationCount = 5,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 8, 8, 6, 42, 17, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 98,
                    PatientName = "Isabella Lee",
                    Diagnosis = "Kidney disease",
                    Status = "Active",
                    MedicationCount = 8,
                    RiskIndicator = "None",
                    AdmissionDate = new DateTime(2025, 11, 4, 2, 35, 50, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 99,
                    PatientName = "Alejandra Camino",
                    Diagnosis = "Asthma",
                    Status = "Under Treatment",
                    MedicationCount = 2,
                    RiskIndicator = "Fall Risk",
                    AdmissionDate = new DateTime(2025, 7, 21, 5, 24, 40, DateTimeKind.Utc),
                },
                new MedicalRecordDto()
                {
                    Id = 100,
                    PatientName = "Howard Snyder",
                    Diagnosis = "Artritis",
                    Status = "Awaiting Diagnosis",
                    MedicationCount = 8,
                    RiskIndicator = "Infection Risk",
                    AdmissionDate = new DateTime(2025, 7, 18, 5, 47, 55, DateTimeKind.Utc),
                },
            };

            return data;
        }
    }

    public class MedicalRecordDto
    {
        public int Id { get; set; }

        public string PatientName { get; set; }

        public string Diagnosis { get; set; }

        public string Status { get; set; }

        public string RiskIndicator { get; set; }

        public int MedicationCount { get; set; }

        public DateTime AdmissionDate { get; set; }

        public string Info { get; set; }
    }
}
