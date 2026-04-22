# CvCheck

CvCheck is an application for adapting resumes to specific job postings with more relevance, more clarity, and less manual effort. The goal is not to invent a "prettier" resume, but to help the candidate present their real experience in a way that is more aligned with the position they want.

## Overview

Applying for jobs usually means rewriting the same resume several times, adjusting the summary, highlighting different skills, and trying to guess what matters most in each posting. This process is slow, repetitive, and often leads to generic resumes.

CvCheck solves this by combining a base candidate profile with the text of a job posting. The application analyzes the vacancy, identifies the most relevant signals, generates an adapted version of the resume, and shows exactly what changed and why.

## Main Flow

The ideal user flow is simple:

1. The user creates a base profile with summary, experience, education, and skills.
2. The user pastes the full job posting text into the application.
3. The app analyzes the job requirements and relevant keywords.
4. The app generates an adapted version of the resume based on the target role.
5. The app highlights changes, explains the reasoning, and flags anything that needs confirmation.
6. The user reviews the final result, edits if needed, and exports the resume in PDF.

## Product Value

The main value of CvCheck is not only speed. The product is designed to increase confidence in the final resume.

Instead of producing artificial text or exaggerating experience, the application should help the candidate:

- present their background in a more targeted way
- emphasize what is relevant for a specific role
- understand what was changed
- identify what is missing for that vacancy
- keep control over the final version before sending it

## Core Capabilities

The application is expected to support these capabilities:

- extraction of job-posting keywords
- identification of main requirements
- professional summary suggestions tailored to the role
- rewriting of experience descriptions with focus on the position
- highlighting of relevant skills
- tone adjustment, such as simple, corporate, or technical
- compatibility score between resume and vacancy
- warnings about missing or weak points in the current resume
- PDF generation
- future option to generate a cover letter

## MVP Scope

The first version should stay lean and focused. The MVP includes:

- basic candidate profile registration
- manual entry of experience, education, and skills
- a field to paste the job posting text
- AI-based extraction of keywords and important signals from the vacancy
- adaptation of the summary and experience descriptions
- preview of the adapted resume
- PDF export

The MVP should avoid unnecessary complexity in the beginning. That means keeping out, for now:

- advanced external integrations
- complex file ingestion and parsing flows
- multiple resume templates with high customization
- fully automated application pipelines
- broader career-planning features

## Product Principles

CvCheck should be guided by a few non-negotiable principles:

### 1. Preserve the candidate's truth

The system must not invent experience, certifications, results, or technologies that the candidate did not provide.

### 2. Avoid exaggeration

The product should help the candidate communicate better, not distort reality to look more impressive.

### 3. Show safe suggestions

Suggestions should be framed as assistance, not as unquestionable output. The candidate remains responsible for the final content.

### 4. Mark uncertain content

Whenever a suggestion depends on inference or interpretation, the system should clearly flag that section for confirmation.

### 5. Explain the changes

The product should make it easy to understand what was changed in the adapted resume and why those changes were suggested.

## Direction for the Application

At a high level, CvCheck should be built as an application centered on three pillars:

- a reusable base profile for the candidate
- an analysis step based on the target vacancy
- a transparent adaptation and review flow before export

The application should prioritize clarity and trust over automation for its own sake. A shorter but reliable result is more valuable than an impressive output that feels artificial.

## Next Steps

This README is the general product reference for the application. From this foundation, the project can later derive features, user stories, tasks, and technical decisions with a clearer sense of scope and product intent.
